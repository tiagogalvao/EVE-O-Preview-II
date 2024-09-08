using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using EveOPreview.Configuration.Interface;
using EveOPreview.Core.Action;
using EveOPreview.Core.Configuration.Interface;
using EveOPreview.Mediator.Messages.Configuration;
using EveOPreview.Mediator.Messages.Services;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Presenters.Interface;
using EveOPreview.View.Implementation;
using EveOPreview.View.Interface;
using MediatR;

namespace EveOPreview.Presenters.Implementation
{
    public class MainFormPresenter : Presenter<IMainFormView>, IMainFormPresenter
    {
        #region Private constants

        private const string VERSION = "7.1.2-alpha";
        private const string FORUM_URL = @"https://forums.eveonline.com/t/eve-o-preview-v5-1-2-fork-multi-client-preview-switcher-2022-05-09-limited-linux-support/361804";

        #endregion

        #region Private fields

        private readonly IMediator _mediator;
        private readonly IThumbnailConfiguration _configuration;
        private readonly IStorage _configurationStorage;
        private readonly IDictionary<string, IThumbnailDescription> _descriptionsCache;
        
        private bool _suppressSizeNotifications;
        private bool _exitApplication;

        #endregion

        public MainFormPresenter(IMainFormView view, IMediator mediator, IThumbnailConfiguration configuration, IStorage configurationStorage)
            : base(view)
        {
            _mediator = mediator;
            _configuration = configuration;
            _configurationStorage = configurationStorage;

            _descriptionsCache = new Dictionary<string, IThumbnailDescription>();

            _suppressSizeNotifications = false;
            _exitApplication = false;

            View.FormActivated = Activate;
            View.FormMinimized = Minimize;
            View.FormCloseRequested = Close;
            View.ApplicationSettingsChanged = SaveApplicationSettings;
            View.ThumbnailsSizeChanged = UpdateThumbnailsSize;
            View.ThumbnailStateChanged = UpdateThumbnailState;
            View.DocumentationLinkActivated = OpenDocumentationLink;
            View.ApplicationExitRequested = ExitApplication;
        }

        private void Activate()
        {
            _suppressSizeNotifications = true;
            LoadApplicationSettings();
            View.SetDocumentationUrl(FORUM_URL);
            View.SetVersionInfo(GetApplicationVersion());
            if (_configuration.MinimizeToTray)
            {
                View.Minimize();
            }

            _mediator.Send(new StartService());
            _suppressSizeNotifications = false;
        }

        private void Minimize()
        {
            if (!_configuration.MinimizeToTray)
            {
                return;
            }

            View.Hide();
        }

        private void Close(ViewCloseRequest request)
        {
            if (_exitApplication || !View.MinimizeToTray)
            {
                _mediator.Send(new StopService()).Wait();

                _configurationStorage.Save();
                request.Allow = true;
                Environment.Exit(0); // TODO: umm.... check later
                return;
            }

            request.Allow = false;
            View.Minimize();
        }

        private async void UpdateThumbnailsSize()
        {
            if (!_suppressSizeNotifications)
            {
                SaveApplicationSettings();
                await _mediator.Publish(new ThumbnailConfiguredSizeUpdated());
            }
        }

        private void LoadApplicationSettings()
        {
            _configurationStorage.Load();

            View.MinimizeToTray = _configuration.MinimizeToTray;

            View.ThumbnailOpacity = _configuration.ThumbnailOpacity;

            View.EnableClientLayoutTracking = _configuration.EnableClientLayoutTracking;
            View.HideActiveClientThumbnail = _configuration.HideActiveClientThumbnail;
            View.MinimizeInactiveClients = _configuration.MinimizeInactiveClients;
            View.ShowThumbnailsAlwaysOnTop = _configuration.ShowThumbnailsAlwaysOnTop;
            View.HideThumbnailsOnLostFocus = _configuration.HideThumbnailsOnLostFocus;
            View.EnablePerClientThumbnailLayouts = _configuration.EnablePerClientThumbnailLayouts;

            View.SetThumbnailSizeLimitations(_configuration.ThumbnailMinimumSize, _configuration.ThumbnailMaximumSize);
            View.ThumbnailSize = _configuration.ThumbnailSize;
            View.ThumbnailFontSize = _configuration.ThumbnailFontSize;

            View.EnableThumbnailZoom = _configuration.ThumbnailZoomEnabled;
            View.ThumbnailZoomFactor = _configuration.ThumbnailZoomFactor;
            View.ThumbnailZoomAnchor = _configuration.ThumbnailZoomAnchor;

            View.ShowThumbnailOverlays = _configuration.ShowThumbnailOverlays;
            View.ShowThumbnailFrames = _configuration.ShowThumbnailFrames;
            View.EnableActiveClientHighlight = _configuration.EnableActiveClientHighlight;
            View.ActiveClientHighlightColor = _configuration.ActiveClientHighlightColor;
        }

        private async void SaveApplicationSettings()
        {
            _configuration.MinimizeToTray = View.MinimizeToTray;

            _configuration.ThumbnailOpacity = (float)View.ThumbnailOpacity;

            _configuration.EnableClientLayoutTracking = View.EnableClientLayoutTracking;
            _configuration.HideActiveClientThumbnail = View.HideActiveClientThumbnail;
            _configuration.MinimizeInactiveClients = View.MinimizeInactiveClients;
            _configuration.ShowThumbnailsAlwaysOnTop = View.ShowThumbnailsAlwaysOnTop;
            _configuration.HideThumbnailsOnLostFocus = View.HideThumbnailsOnLostFocus;
            _configuration.EnablePerClientThumbnailLayouts = View.EnablePerClientThumbnailLayouts;

            _configuration.ThumbnailSize = View.ThumbnailSize;
            _configuration.ThumbnailFontSize = View.ThumbnailFontSize;

            _configuration.ThumbnailZoomEnabled = View.EnableThumbnailZoom;
            _configuration.ThumbnailZoomFactor = View.ThumbnailZoomFactor;
            _configuration.ThumbnailZoomAnchor = View.ThumbnailZoomAnchor;

            _configuration.ShowThumbnailOverlays = View.ShowThumbnailOverlays;
            if (_configuration.ShowThumbnailFrames != View.ShowThumbnailFrames)
            {
                _configuration.ShowThumbnailFrames = View.ShowThumbnailFrames;
                await _mediator.Publish(new ThumbnailFrameSettingsUpdated());
            }

            _configuration.EnableActiveClientHighlight = View.EnableActiveClientHighlight;
            _configuration.ActiveClientHighlightColor = View.ActiveClientHighlightColor;

            _configurationStorage.Save();

            View.RefreshZoomSettings();

            await _mediator.Send(new SaveConfiguration());
        }

        public void AddThumbnails(IList<string> thumbnailTitles)
        {
            IList<IThumbnailDescription> descriptions = new List<IThumbnailDescription>(thumbnailTitles.Count);
            lock (_descriptionsCache)
            {
                foreach (string title in thumbnailTitles)
                {
                    if (!_descriptionsCache.ContainsKey(title))
                    {
                        IThumbnailDescription description = CreateThumbnailDescription(title);
                        _descriptionsCache[title] = description;

                        descriptions.Add(description);
                    }
                }
            }
            View.AddThumbnails(descriptions);
        }

        public void RemoveThumbnails(IList<string> thumbnailTitles)
        {
            IList<IThumbnailDescription> descriptions = new List<IThumbnailDescription>(thumbnailTitles.Count);
            lock (_descriptionsCache)
            {
                foreach (string title in thumbnailTitles)
                {
                    if (!_descriptionsCache.Remove(title, out IThumbnailDescription description))
                    {
                        continue;
                    }

                    descriptions.Add(description);
                }
            }

            View.RemoveThumbnails(descriptions);
        }

        private IThumbnailDescription CreateThumbnailDescription(string title)
        {
            bool isDisabled = _configuration.IsThumbnailDisabled(title);
            return new ThumbnailDescription(title, isDisabled);
        }

        private async void UpdateThumbnailState(string title)
        {
            if (_descriptionsCache.TryGetValue(title, out IThumbnailDescription description))
            {
                _configuration.ToggleThumbnail(title, description.IsDisabled);
            }

            await _mediator.Send(new SaveConfiguration());
        }

        public void UpdateThumbnailSize(Size size)
        {
            _suppressSizeNotifications = true;
            View.ThumbnailSize = size;
            _suppressSizeNotifications = false;
        }

        private void OpenDocumentationLink()
        {
            // TODO Move out to a separate service / presenter / message handler
            var documentationPsi = new ProcessStartInfo
            {
                FileName = new Uri(FORUM_URL).AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(documentationPsi);
        }

        private string GetApplicationVersion()
        {
            //TODO: Check why version is not being read properly
            return VERSION;
        }

        private void ExitApplication()
        {
            _exitApplication = true;
            View.Close();
        }
    }
}