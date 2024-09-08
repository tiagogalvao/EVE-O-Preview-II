using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Threading;
using EveOPreview.Configuration.Interface;
using EveOPreview.Mediator.Messages.Configuration;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Services.Interface;
using EveOPreview.View.Interface;
using MediatR;

namespace EveOPreview.Services.Implementation
{
    internal sealed class ThumbnailManager : IThumbnailManager
    {
        #region Private constants

        private const int WINDOW_POSITION_THRESHOLD_LOW = -10_000;
        private const int WINDOW_POSITION_THRESHOLD_HIGH = 31_000;
        private const int WINDOW_SIZE_THRESHOLD = 10;
        private const int FORCED_REFRESH_CYCLE_THRESHOLD = 2;
        private const int DEFAULT_LOCATION_CHANGE_NOTIFICATION_DELAY = 2;

        private const string DEFAULT_CLIENT_TITLE = "EVE";

        #endregion

        #region Private fields

        private readonly IMediator _mediator;
        private readonly IProcessMonitor _processMonitor;
        private readonly IWindowManager _windowManager;
        private readonly IThumbnailConfiguration _configuration;
        private readonly DispatcherTimer _thumbnailUpdateTimer;
        private readonly IThumbnailViewFactory _thumbnailViewFactory;
        private readonly Dictionary<IntPtr, IThumbnailView> _thumbnailViews;

        private (IntPtr Handle, string Title) _activeClient;
        private IntPtr _externalApplication;

        private readonly object _locationChangeNotificationSyncRoot;
        private (IntPtr Handle, string Title, string ActiveClient, Point Location, int Delay) _enqueuedLocationChangeNotification;

        private bool _ignoreViewEvents;
        private bool _isHoverEffectActive;

        private int _refreshCycleCount;
        private int _hideThumbnailsDelay;

        #endregion

        public ThumbnailManager(IMediator mediator, IThumbnailConfiguration configuration, IProcessMonitor processMonitor, IWindowManager windowManager, IThumbnailViewFactory factory)
        {
            _mediator = mediator;
            _processMonitor = processMonitor;
            _windowManager = windowManager;
            _configuration = configuration;
            _thumbnailViewFactory = factory;

            _activeClient = (IntPtr.Zero, DEFAULT_CLIENT_TITLE);

            EnableViewEvents();
            _isHoverEffectActive = false;

            _refreshCycleCount = 0;
            _locationChangeNotificationSyncRoot = new object();
            _enqueuedLocationChangeNotification = (IntPtr.Zero, null, null, Point.Empty, -1);

            _thumbnailViews = new Dictionary<IntPtr, IThumbnailView>();

            //  DispatcherTimer setup
            _thumbnailUpdateTimer = new DispatcherTimer();
            _thumbnailUpdateTimer.Tick += ThumbnailUpdateTimerTick;
            _thumbnailUpdateTimer.Interval = new TimeSpan(0, 0, 0, 0, configuration.ThumbnailRefreshPeriod);

            _hideThumbnailsDelay = _configuration.HideThumbnailsDelay;
        }

        public void Start()
        {
            _thumbnailUpdateTimer.Start();

            RefreshThumbnails();
        }

        public void Stop()
        {
            _thumbnailUpdateTimer.Stop();
        }

        private void ThumbnailUpdateTimerTick(object sender, EventArgs e)
        {
            UpdateThumbnailsList();
            RefreshThumbnails();
        }

        private async void UpdateThumbnailsList()
        {
            _processMonitor.GetUpdatedProcesses(out ICollection<IProcessInfo> addedProcesses, out ICollection<IProcessInfo> updatedProcesses, out ICollection<IProcessInfo> removedProcesses);

            List<string> viewsAdded = new List<string>();
            List<string> viewsRemoved = new List<string>();

            foreach (IProcessInfo process in addedProcesses)
            {
                IThumbnailView view = _thumbnailViewFactory.Create(process.Handle, process.Title, _configuration.ThumbnailSize);
                view.IsOverlayEnabled = _configuration.ShowThumbnailOverlays;
                view.SetFrames(_configuration.ShowThumbnailFrames);
                // Max/Min size limitations should be set AFTER the frames are disabled
                // Otherwise thumbnail window will be unnecessary resized
                view.SetSizeLimitations(_configuration.ThumbnailMinimumSize, _configuration.ThumbnailMaximumSize);
                view.SetTopMost(_configuration.ShowThumbnailsAlwaysOnTop);

                view.ThumbnailLocation = IsManageableThumbnail(view)
                    ? _configuration.GetThumbnailLocation(view.Title, _activeClient.Title, view.ThumbnailLocation)
                    : _configuration.GetDefaultThumbnailLocation();

                _thumbnailViews.Add(view.Id, view);

                view.ThumbnailResized = ThumbnailViewResized;
                view.ThumbnailMoved = ThumbnailViewMoved;
                view.ThumbnailFocused = ThumbnailViewFocused;
                view.ThumbnailLostFocus = ThumbnailViewLostFocus;
                view.ThumbnailActivated = ThumbnailActivated;
                view.ThumbnailDeactivated = ThumbnailDeactivated;

                view.RegisterHotkey(_configuration.GetClientHotkey(view.Title));

                ApplyClientLayout(view.Id, view.Title);

                // TODO Add extension filter here later
                if (view.Title != DEFAULT_CLIENT_TITLE)
                {
                    viewsAdded.Add(view.Title);
                }
            }

            foreach (IProcessInfo process in updatedProcesses)
            {
                _thumbnailViews.TryGetValue(process.Handle, out IThumbnailView view);

                if (view == null)
                {
                    // Something went terribly wrong
                    continue;
                }

                if (process.Title != view.Title) // update thumbnail title
                {
                    viewsRemoved.Add(view.Title);
                    view.Title = process.Title;
                    viewsAdded.Add(view.Title);

                    view.RegisterHotkey(_configuration.GetClientHotkey(process.Title));

                    ApplyClientLayout(view.Id, view.Title);
                }
            }

            foreach (IProcessInfo process in removedProcesses)
            {
                IThumbnailView view = _thumbnailViews[process.Handle];

                _thumbnailViews.Remove(view.Id);
                if (view.Title != DEFAULT_CLIENT_TITLE)
                {
                    viewsRemoved.Add(view.Title);
                }

                view.UnregisterHotkey();

                view.ThumbnailResized = null;
                view.ThumbnailMoved = null;
                view.ThumbnailFocused = null;
                view.ThumbnailLostFocus = null;
                view.ThumbnailActivated = null;

                view.Close();
            }

            if (viewsAdded.Count > 0 || viewsRemoved.Count > 0)
            {
                await _mediator.Publish(new ThumbnailListUpdated(viewsAdded, viewsRemoved));
            }
        }

        private void RefreshThumbnails()
        {
            // TODO Split this method
            IntPtr foregroundWindowHandle = _windowManager.GetForegroundWindowHandle();

            // The foreground window can be NULL in certain circumstances, such as when a window is losing activation.
            // It is safer to just skip this refresh round than to do something while the system state is undefined
            if (foregroundWindowHandle == IntPtr.Zero)
            {
                return;
            }

            string foregroundWindowTitle = null;

            // Check if the foreground window handle is one of the known handles for client windows or their thumbnails
            bool isClientWindow = IsClientWindowActive(foregroundWindowHandle);
            bool isMainWindowActive = IsMainWindowActive(foregroundWindowHandle);

            if (foregroundWindowHandle == _activeClient.Handle)
            {
                foregroundWindowTitle = _activeClient.Title;
            }
            else if (_thumbnailViews.TryGetValue(foregroundWindowHandle, out IThumbnailView foregroundView))
            {
                // This code will work only on Alt+Tab switch between clients
                foregroundWindowTitle = foregroundView.Title;
            }
            else if (!isClientWindow)
            {
                _externalApplication = foregroundWindowHandle;
            }

            // No need to minimize EVE clients when switching out to non-EVE window (like thumbnail)
            if (!string.IsNullOrEmpty(foregroundWindowTitle))
            {
                SwitchActiveClient(foregroundWindowHandle, foregroundWindowTitle);
            }

            bool hideAllThumbnails = _configuration.HideThumbnailsOnLostFocus && !(isClientWindow || isMainWindowActive);

            // Wait for some time before hiding all previews
            if (hideAllThumbnails)
            {
                _hideThumbnailsDelay--;
                if (_hideThumbnailsDelay > 0)
                {
                    hideAllThumbnails = false; // Postpone the 'hide all' operation
                }
                else
                {
                    _hideThumbnailsDelay = 0; // Stop the counter
                }
            }
            else
            {
                _hideThumbnailsDelay = _configuration.HideThumbnailsDelay; // Reset the counter
            }

            _refreshCycleCount++;

            bool forceRefresh;
            if (_refreshCycleCount >= FORCED_REFRESH_CYCLE_THRESHOLD)
            {
                _refreshCycleCount = 0;
                forceRefresh = true;
            }
            else
            {
                forceRefresh = false;
            }

            DisableViewEvents();

            // Snap thumbnail
            // No need to update Thumbnails while one of them is highlighted
            if ((!_isHoverEffectActive) && TryDequeueLocationChange(out var locationChange))
            {
                if ((locationChange.ActiveClient == _activeClient.Title) && _thumbnailViews.TryGetValue(locationChange.Handle, out var view))
                {
                    SnapThumbnailView(view);

                    RaiseThumbnailLocationUpdatedNotification(view.Title);
                }
                else
                {
                    RaiseThumbnailLocationUpdatedNotification(locationChange.Title);
                }
            }

            // Hide, show, resize and move
            foreach (KeyValuePair<IntPtr, IThumbnailView> entry in _thumbnailViews)
            {
                IThumbnailView view = entry.Value;

                if (hideAllThumbnails || _configuration.IsThumbnailDisabled(view.Title))
                {
                    if (view.IsActive)
                    {
                        view.Hide();
                    }

                    continue;
                }

                if (_configuration.HideActiveClientThumbnail && (view.Id == _activeClient.Handle))
                {
                    if (view.IsActive)
                    {
                        view.Hide();
                    }

                    continue;
                }

                // No need to update Thumbnails while one of them is highlighted
                if (!_isHoverEffectActive)
                {
                    // Do not even move thumbnails with default caption
                    if (IsManageableThumbnail(view))
                    {
                        view.ThumbnailLocation = _configuration.GetThumbnailLocation(view.Title, _activeClient.Title, view.ThumbnailLocation);
                    }

                    view.SetOpacity(_configuration.ThumbnailOpacity);
                    view.SetTopMost(_configuration.ShowThumbnailsAlwaysOnTop);
                }

                view.IsOverlayEnabled = _configuration.ShowThumbnailOverlays;

                view.SetHighlight(_configuration.EnableActiveClientHighlight && (view.Id == _activeClient.Handle),
                    _configuration.ActiveClientHighlightColor, _configuration.ActiveClientHighlightThickness);

                if (!view.IsActive)
                {
                    view.Show();
                }
                else
                {
                    view.Refresh(forceRefresh);
                }
            }

            EnableViewEvents();
        }

        public void UpdateThumbnailsSize()
        {
            SetThumbnailsSize(_configuration.ThumbnailSize);
        }

        private void SetThumbnailsSize(Size size)
        {
            DisableViewEvents();

            foreach (KeyValuePair<IntPtr, IThumbnailView> entry in _thumbnailViews)
            {
                entry.Value.ThumbnailSize = size;
                entry.Value.Refresh(false);
            }

            EnableViewEvents();
        }

        public void UpdateThumbnailFrames()
        {
            DisableViewEvents();

            foreach (KeyValuePair<IntPtr, IThumbnailView> entry in _thumbnailViews)
            {
                entry.Value.SetFrames(_configuration.ShowThumbnailFrames);
            }

            EnableViewEvents();
        }

        private void EnableViewEvents()
        {
            _ignoreViewEvents = false;
        }

        private void DisableViewEvents()
        {
            _ignoreViewEvents = true;
        }

        private void SwitchActiveClient(IntPtr foregroundClientHandle, string foregroundClientTitle)
        {
            // Check if any actions are needed
            if (_activeClient.Handle == foregroundClientHandle)
            {
                return;
            }

            // Minimize the currently active client if needed
            if (_configuration.MinimizeInactiveClients && !_configuration.IsPriorityClient(_activeClient.Title))
            {
                _windowManager.MinimizeWindow(_activeClient.Handle, false);
            }

            _activeClient = (foregroundClientHandle, foregroundClientTitle);
        }

        private void ThumbnailViewFocused(IntPtr id)
        {
            if (_isHoverEffectActive)
            {
                return;
            }

            _isHoverEffectActive = true;

            IThumbnailView view = _thumbnailViews[id];

            view.SetTopMost(true);
            view.SetOpacity(1.0);

            if (_configuration.ThumbnailZoomEnabled)
            {
                ThumbnailZoomIn(view);
            }
        }

        private void ThumbnailViewLostFocus(IntPtr id)
        {
            if (!_isHoverEffectActive)
            {
                return;
            }

            IThumbnailView view = _thumbnailViews[id];

            if (_configuration.ThumbnailZoomEnabled)
            {
                ThumbnailZoomOut(view);
            }

            view.SetOpacity(_configuration.ThumbnailOpacity);

            _isHoverEffectActive = false;
        }

        private void ThumbnailActivated(IntPtr id)
        {
            IThumbnailView view = _thumbnailViews[id];

            Task.Run(() => { _windowManager.ActivateWindow(view.Id); })
                .ContinueWith((task) =>
                {
                    // This code should be executed on UI thread
                    SwitchActiveClient(view.Id, view.Title);
                    UpdateClientLayouts();
                    RefreshThumbnails();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ThumbnailDeactivated(IntPtr id, bool switchOut)
        {
            if (switchOut)
            {
                _windowManager.ActivateWindow(_externalApplication);
            }
            else
            {
                if (!_thumbnailViews.TryGetValue(id, out IThumbnailView view))
                {
                    return;
                }

                _windowManager.MinimizeWindow(view.Id, true);
                RefreshThumbnails();
            }
        }

        private async void ThumbnailViewResized(IntPtr id)
        {
            if (_ignoreViewEvents)
            {
                return;
            }

            IThumbnailView view = _thumbnailViews[id];

            SetThumbnailsSize(view.ThumbnailSize);

            view.Refresh(false);

            await _mediator.Publish(new ThumbnailActiveSizeUpdated(view.ThumbnailSize));
        }

        private void ThumbnailViewMoved(IntPtr id)
        {
            if (_ignoreViewEvents)
            {
                return;
            }

            IThumbnailView view = _thumbnailViews[id];
            view.Refresh(false);
            EnqueueLocationChange(view);
        }

        // Checks whether currently active window belongs to an EVE client or its thumbnail
        private bool IsClientWindowActive(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
            {
                return false;
            }

            foreach (KeyValuePair<IntPtr, IThumbnailView> entry in _thumbnailViews)
            {
                IThumbnailView view = entry.Value;

                if (view.IsKnownHandle(windowHandle))
                {
                    return true;
                }
            }

            return false;
        }

        // Check whether the currently active window belongs to EVE-O Preview itself
        private bool IsMainWindowActive(IntPtr windowHandle)
        {
            return (_processMonitor.GetMainProcess().Handle == windowHandle);
        }

        private void ThumbnailZoomIn(IThumbnailView view)
        {
            DisableViewEvents();

            view.ZoomIn(_configuration.ThumbnailZoomAnchor, _configuration.ThumbnailZoomFactor);
            view.Refresh(false);

            EnableViewEvents();
        }

        private void ThumbnailZoomOut(IThumbnailView view)
        {
            DisableViewEvents();

            view.ZoomOut();
            view.Refresh(false);

            EnableViewEvents();
        }

        private void SnapThumbnailView(IThumbnailView view)
        {
            // Check if this feature is enabled
            if (!_configuration.EnableThumbnailSnap)
            {
                return;
            }

            // Only borderless thumbnails can be docked
            if (_configuration.ShowThumbnailFrames)
            {
                return;
            }

            int width = _configuration.ThumbnailSize.Width;
            int height = _configuration.ThumbnailSize.Height;

            // TODO Extract method
            int baseX = view.ThumbnailLocation.X;
            int baseY = view.ThumbnailLocation.Y;

            Point[] viewPoints = { new Point(baseX, baseY), new Point(baseX + width, baseY), new Point(baseX, baseY + height), new Point(baseX + width, baseY + height) };

            // TODO Extract constants
            int thresholdX = Math.Max(20, width / 10);
            int thresholdY = Math.Max(20, height / 10);

            foreach (var entry in _thumbnailViews)
            {
                IThumbnailView testView = entry.Value;

                if (view.Id == testView.Id)
                {
                    continue;
                }

                int testX = testView.ThumbnailLocation.X;
                int testY = testView.ThumbnailLocation.Y;

                Point[] testPoints = { new Point(testX, testY), new Point(testX + width, testY), new Point(testX, testY + height), new Point(testX + width, testY + height) };

                var delta = TestViewPoints(viewPoints, testPoints, thresholdX, thresholdY);

                if ((delta.X == 0) && (delta.Y == 0))
                {
                    continue;
                }

                view.ThumbnailLocation = new Point(view.ThumbnailLocation.X + delta.X, view.ThumbnailLocation.Y + delta.Y);
                _configuration.SetThumbnailLocation(view.Title, _activeClient.Title, view.ThumbnailLocation);
                break;
            }
        }

        private static (int X, int Y) TestViewPoints(Point[] viewPoints, Point[] testPoints, int thresholdX, int thresholdY)
        {
            // Point combinations that we need to check
            // No need to check all 4x4 combinations
            (int ViewOffset, int TestOffset)[] testOffsets =
            {
                (0, 3), (0, 2), (1, 2),
                (0, 1), (0, 0), (1, 0),
                (2, 1), (2, 0), (3, 0)
            };

            foreach (var testOffset in testOffsets)
            {
                Point viewPoint = viewPoints[testOffset.ViewOffset];
                Point testPoint = testPoints[testOffset.TestOffset];

                int deltaX = testPoint.X - viewPoint.X;
                int deltaY = testPoint.Y - viewPoint.Y;

                if ((Math.Abs(deltaX) <= thresholdX) && (Math.Abs(deltaY) <= thresholdY))
                {
                    return (deltaX, deltaY);
                }
            }

            return (0, 0);
        }

        private void ApplyClientLayout(IntPtr clientHandle, string clientTitle)
        {
            if (!_configuration.EnableClientLayoutTracking)
            {
                return;
            }

            // No need to apply layout for not yet logged-in clients
            if (clientTitle == DEFAULT_CLIENT_TITLE)
            {
                return;
            }

            Core.Model.Client.Layout clientLayout = _configuration.GetClientLayout(clientTitle);

            if (clientLayout == null)
            {
                return;
            }

            if (clientLayout.IsMaximized)
            {
                _windowManager.MaximizeWindow(clientHandle);
            }
            else
            {
                _windowManager.MoveWindow(clientHandle, clientLayout.Location.X, clientLayout.Location.Y, clientLayout.Size.Width, clientLayout.Size.Height);
            }
        }

        private void UpdateClientLayouts()
        {
            if (!_configuration.EnableClientLayoutTracking)
            {
                return;
            }

            foreach (KeyValuePair<IntPtr, IThumbnailView> entry in _thumbnailViews)
            {
                IThumbnailView view = entry.Value;

                // No need to save layout for not yet logged-in clients
                if (view.Title == DEFAULT_CLIENT_TITLE)
                {
                    continue;
                }

                (int Left, int Top, int Right, int Bottom) position = _windowManager.GetWindowPosition(view.Id);
                int width = Math.Abs(position.Right - position.Left);
                int height = Math.Abs(position.Bottom - position.Top);

                if (!(_windowManager.IsWindowMaximized(view.Id) || IsValidWindowPosition(position.Left, position.Top, width, height)))
                {
                    continue;
                }

                _configuration.SetClientLayout(view.Title, new Core.Model.Client.Layout(position.Left, position.Top, width, height, _windowManager.IsWindowMaximized(view.Id)));
            }
        }

        private void EnqueueLocationChange(IThumbnailView view)
        {
            string activeClientTitle = _activeClient.Title;
            // TODO ??
            _configuration.SetThumbnailLocation(view.Title, activeClientTitle, view.ThumbnailLocation);

            lock (_locationChangeNotificationSyncRoot)
            {
                if (_enqueuedLocationChangeNotification.Handle == IntPtr.Zero)
                {
                    _enqueuedLocationChangeNotification = (view.Id, view.Title, activeClientTitle, view.ThumbnailLocation, DEFAULT_LOCATION_CHANGE_NOTIFICATION_DELAY);
                    return;
                }

                // Reset the delay and exit
                if ((_enqueuedLocationChangeNotification.Handle == view.Id) &&
                    (_enqueuedLocationChangeNotification.ActiveClient == activeClientTitle))
                {
                    _enqueuedLocationChangeNotification.Delay = DEFAULT_LOCATION_CHANGE_NOTIFICATION_DELAY;
                    return;
                }

                RaiseThumbnailLocationUpdatedNotification(_enqueuedLocationChangeNotification.Title);
                _enqueuedLocationChangeNotification = (view.Id, view.Title, activeClientTitle, view.ThumbnailLocation, DEFAULT_LOCATION_CHANGE_NOTIFICATION_DELAY);
            }
        }

        private bool TryDequeueLocationChange(out (IntPtr Handle, string Title, string ActiveClient, Point Location) change)
        {
            lock (_locationChangeNotificationSyncRoot)
            {
                change = (IntPtr.Zero, null, null, Point.Empty);

                if (_enqueuedLocationChangeNotification.Handle == IntPtr.Zero)
                {
                    return false;
                }

                _enqueuedLocationChangeNotification.Delay--;

                if (_enqueuedLocationChangeNotification.Delay > 0)
                {
                    return false;
                }

                change = (_enqueuedLocationChangeNotification.Handle, _enqueuedLocationChangeNotification.Title, _enqueuedLocationChangeNotification.ActiveClient, _enqueuedLocationChangeNotification.Location);
                _enqueuedLocationChangeNotification = (IntPtr.Zero, null, null, Point.Empty, -1);

                return true;
            }
        }

        private async void RaiseThumbnailLocationUpdatedNotification(string title)
        {
            if (string.IsNullOrEmpty(title) || (title == DEFAULT_CLIENT_TITLE))
            {
                return;
            }

            await _mediator.Send(new SaveConfiguration());
        }

        // We shouldn't manage some thumbnails (like thumbnail of the EVE client sitting on the login screen)
        // TODO Move to a service (?)
        private bool IsManageableThumbnail(IThumbnailView view)
        {
            return view.Title != DEFAULT_CLIENT_TITLE;
        }

        // Quick sanity check that the window is not minimized
        private bool IsValidWindowPosition(int left, int top, int width, int height)
        {
            return (left > WINDOW_POSITION_THRESHOLD_LOW) 
                   && (left < WINDOW_POSITION_THRESHOLD_HIGH)
                   && (top > WINDOW_POSITION_THRESHOLD_LOW) 
                   && (top < WINDOW_POSITION_THRESHOLD_HIGH)
                   && (width > WINDOW_SIZE_THRESHOLD) 
                   && (height > WINDOW_SIZE_THRESHOLD);
        }
    }
}