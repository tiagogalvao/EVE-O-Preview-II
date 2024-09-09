using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Core.Configuration;
using EveOPreview.Core.Internal.Interop.Windows;
using EveOPreview.Hotkey;
using EveOPreview.Services.Interface;
using EveOPreview.View.Interface;

namespace EveOPreview.View.Implementation
{
    public abstract partial class ThumbnailView : Form, IThumbnailView
    {
        #region Private constants

        private const int RESIZE_EVENT_TIMEOUT = 500;
        private const double OPACITY_THRESHOLD = 0.9;
        private const double OPACITY_EPSILON = 0.1;

        #endregion

        #region Private fields

        private readonly ThumbnailOverlay _overlay;

        // Part of the logic (namely current size / position management)
        // was moved to the view due to the performance reasons
        private bool _isOverlayVisible;
        private bool _isTopMost;
        private bool _isHighlightEnabled;
        private bool _isHighlightRequested;
        private int _highlightWidth;

        private bool _isLocationChanged;
        private bool _isSizeChanged;

        private bool _isCustomMouseModeActive;

        private double _opacity;

        private DateTime _suppressResizeEventsTimestamp;
        private Size _baseZoomSize;
        private Point _baseZoomLocation;
        private Point _baseMousePosition;
        private Size _baseZoomMaximumSize;

        private HotkeyHandler _hotkeyHandler;

        #endregion

        protected ThumbnailView(IWindowManager windowManager)
        {
            SuppressResizeEvent();

            WindowManager = windowManager;

            IsActive = false;

            IsOverlayEnabled = false;
            _isOverlayVisible = false;
            _isTopMost = false;
            _isHighlightEnabled = false;
            _isHighlightRequested = false;

            _isLocationChanged = true;
            _isSizeChanged = true;

            _isCustomMouseModeActive = false;

            _opacity = 0.1;

            InitializeComponent();

            _overlay = new ThumbnailOverlay(this, MouseDown_Handler);
        }

        protected IWindowManager WindowManager { get; }

        public nint Id { get; set; }

        public string Title
        {
            get => Text;
            set
            {
                Text = value;
                _overlay.SetOverlayLabel(value);
            }
        }

        public bool IsActive { get; set; }

        public bool IsOverlayEnabled { get; set; }

        public Point ThumbnailLocation
        {
            get => Location;
            set
            {
                StartPosition = FormStartPosition.Manual;
                Location = value;
            }
        }

        public Size ThumbnailSize
        {
            get => ClientSize;
            set => ClientSize = value;
        }

        public Action<nint> ThumbnailResized { get; set; }

        public Action<nint> ThumbnailMoved { get; set; }

        public Action<nint> ThumbnailFocused { get; set; }

        public Action<nint> ThumbnailLostFocus { get; set; }

        public Action<nint> ThumbnailActivated { get; set; }

        public Action<nint, bool> ThumbnailDeactivated { get; set; }

        public new void Show()
        {
            SuppressResizeEvent();

            base.Show();

            _isLocationChanged = true;
            _isSizeChanged = true;
            _isOverlayVisible = false;

            Refresh(true);

            IsActive = true;
        }

        public new void Hide()
        {
            SuppressResizeEvent();

            IsActive = false;

            _overlay.Hide();
            base.Hide();
        }

        public new virtual void Close()
        {
            SuppressResizeEvent();

            IsActive = false;
            _overlay.Close();
            base.Close();
        }

        // This method is used to determine if the provided Handle is related to client or its thumbnail
        public bool IsKnownHandle(nint handle)
        {
            return (Id == handle) || (Handle == handle) || (_overlay.Handle == handle);
        }

        public void SetSizeLimitations(Size minimumSize, Size maximumSize)
        {
            MinimumSize = minimumSize;
            MaximumSize = maximumSize;
        }

        public void SetOpacity(double opacity)
        {
            if (opacity >= OPACITY_THRESHOLD)
            {
                opacity = 1.0;
            }

            if (Math.Abs(opacity - _opacity) < OPACITY_EPSILON)
            {
                return;
            }

            try
            {
                Opacity = opacity;

                // Overlay opacity settings
                // Of the thumbnail's opacity is almost full then set the overlay's one to
                // full. Otherwise set it to half of the thumbnail opacity
                // Opacity value is stored even if the overlay is not displayed atm
                _overlay.Opacity = opacity > 0.8 ? 1.0 : 1.0 - (1.0 - opacity) / 2;

                _opacity = opacity;
            }
            catch (Win32Exception)
            {
                // Something went wrong in WinForms internals
                // Opacity will be updated in the next cycle
            }
        }

        public void SetFrames(bool enable)
        {
            FormBorderStyle style = enable ? FormBorderStyle.SizableToolWindow : FormBorderStyle.None;

            // No need to change the borders style if it is ALREADY correct
            if (FormBorderStyle == style)
            {
                return;
            }

            SuppressResizeEvent();

            FormBorderStyle = style;
        }

        public void SetTopMost(bool enableTopmost)
        {
            if (_isTopMost == enableTopmost)
            {
                return;
            }

            _overlay.TopMost = enableTopmost;
            TopMost = enableTopmost;

            _isTopMost = enableTopmost;
        }

        public void SetHighlight(bool enabled, Color color, int width)
        {
            if (_isHighlightRequested == enabled)
            {
                return;
            }

            if (enabled)
            {
                _isHighlightRequested = true;
                _highlightWidth = width;
                BackColor = color;
            }
            else
            {
                _isHighlightRequested = false;
                BackColor = SystemColors.Control;
            }

            _isSizeChanged = true;
        }

        public void ZoomIn(ZoomAnchor anchor, int zoomFactor)
        {
            int oldWidth = _baseZoomSize.Width;
            int oldHeight = _baseZoomSize.Height;

            int locationX = Location.X;
            int locationY = Location.Y;

            int clientSizeWidth = ClientSize.Width;
            int clientSizeHeight = ClientSize.Height;
            int newWidth = (zoomFactor * clientSizeWidth) + (Size.Width - clientSizeWidth);
            int newHeight = (zoomFactor * clientSizeHeight) + (Size.Height - clientSizeHeight);

            // First change size, THEN move the window
            // Otherwise there is a chance to fail in a loop
            // Zoom required -> Moved the windows 1st -> Focus is lost -> Window is moved back -> Focus is back on -> Zoom required -> ...
            MaximumSize = new Size(0, 0);
            Size = new Size(newWidth, newHeight);

            switch (anchor)
            {
                case ZoomAnchor.NW:
                    break;
                case ZoomAnchor.N:
                    Location = new Point(locationX - newWidth / 2 + oldWidth / 2, locationY);
                    break;
                case ZoomAnchor.NE:
                    Location = new Point(locationX - newWidth + oldWidth, locationY);
                    break;

                case ZoomAnchor.W:
                    Location = new Point(locationX, locationY - newHeight / 2 + oldHeight / 2);
                    break;
                case ZoomAnchor.C:
                    Location = new Point(locationX - newWidth / 2 + oldWidth / 2, locationY - newHeight / 2 + oldHeight / 2);
                    break;
                case ZoomAnchor.E:
                    Location = new Point(locationX - newWidth + oldWidth, locationY - newHeight / 2 + oldHeight / 2);
                    break;

                case ZoomAnchor.SW:
                    Location = new Point(locationX, locationY - newHeight + _baseZoomSize.Height);
                    break;
                case ZoomAnchor.S:
                    Location = new Point(locationX - newWidth / 2 + oldWidth / 2, locationY - newHeight + oldHeight);
                    break;
                case ZoomAnchor.SE:
                    Location = new Point(locationX - newWidth + oldWidth, locationY - newHeight + oldHeight);
                    break;
            }
        }

        public void ZoomOut()
        {
            RestoreWindowSizeAndLocation();
        }

        public void RegisterHotkey(Keys hotkey)
        {
            if (_hotkeyHandler != null)
            {
                UnregisterHotkey();
            }

            if (hotkey == Keys.None)
            {
                return;
            }

            _hotkeyHandler = new HotkeyHandler(Handle, hotkey);
            if (_hotkeyHandler.CanRegister())
            {
                _hotkeyHandler.Pressed += HotkeyPressed_Handler;
                _hotkeyHandler.Register();
            }
        }

        public void UnregisterHotkey()
        {
            if (_hotkeyHandler == null)
            {
                return;
            }

            _hotkeyHandler.Unregister();
            _hotkeyHandler.Pressed -= HotkeyPressed_Handler;
            _hotkeyHandler.Dispose();
            _hotkeyHandler = null;
        }

        public void Refresh(bool forceRefresh)
        {
            RefreshThumbnail(forceRefresh);
            HighlightThumbnail(forceRefresh || _isSizeChanged);
            RefreshOverlay(forceRefresh || _isSizeChanged || _isLocationChanged);

            _isSizeChanged = false;
        }

        protected abstract void RefreshThumbnail(bool forceRefresh);

        protected abstract void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft);

        private void HighlightThumbnail(bool forceRefresh)
        {
            if (!forceRefresh && (_isHighlightRequested == _isHighlightEnabled))
            {
                // Nothing to do here
                return;
            }

            _isHighlightEnabled = _isHighlightRequested;

            int baseWidth = ClientSize.Width;
            int baseHeight = ClientSize.Height;

            if (!_isHighlightRequested)
            {
                //No highlighting enabled, so no math required
                ResizeThumbnail(baseWidth, baseHeight, 0, 0, 0, 0);
                return;
            }

            double baseAspectRatio = ((double)baseWidth) / baseHeight;

            int actualHeight = baseHeight - 2 * _highlightWidth;
            double desiredWidth = actualHeight * baseAspectRatio;
            int actualWidth = (int)Math.Round(desiredWidth, MidpointRounding.AwayFromZero);
            int highlightWidthLeft = (baseWidth - actualWidth) / 2;
            int highlightWidthRight = baseWidth - actualWidth - highlightWidthLeft;

            ResizeThumbnail(ClientSize.Width, ClientSize.Height, _highlightWidth, highlightWidthRight, _highlightWidth, highlightWidthLeft);
        }

        private void RefreshOverlay(bool forceRefresh)
        {
            if (_isOverlayVisible && !forceRefresh)
            {
                // No need to update anything. Everything is already set up
                return;
            }

            _overlay.EnableOverlayLabel(IsOverlayEnabled);

            if (!_isOverlayVisible)
            {
                // One-time action to show the Overlay before it is set up
                // Otherwise its position won't be set
                _overlay.Show();
                _isOverlayVisible = true;
            }

            Size overlaySize = ClientSize;
            Point overlayLocation = Location;

            int borderWidth = (Size.Width - ClientSize.Width) / 2;
            overlayLocation.X += borderWidth;
            overlayLocation.Y += (Size.Height - ClientSize.Height) - borderWidth;

            _isLocationChanged = false;
            _overlay.Size = overlaySize;
            _overlay.Location = overlayLocation;
            _overlay.Refresh();
        }

        private void SuppressResizeEvent()
        {
            // Workaround for WinForms issue with the Resize event being fired with inconsistent ClientSize value
            // Any Resize events fired before this timestamp will be ignored
            _suppressResizeEventsTimestamp = DateTime.UtcNow.AddMilliseconds(RESIZE_EVENT_TIMEOUT);
        }

        #region GUI events

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= (int)InteropConstants.WS_EX_TOOLWINDOW;
                return createParams;
            }
        }

        private void Move_Handler(object sender, EventArgs e)
        {
            _isLocationChanged = true;
            ThumbnailMoved?.Invoke(Id);
        }

        private void Resize_Handler(object sender, EventArgs e)
        {
            if (DateTime.UtcNow < _suppressResizeEventsTimestamp)
            {
                return;
            }

            _isSizeChanged = true;

            ThumbnailResized?.Invoke(Id);
        }

        private void MouseEnter_Handler(object sender, EventArgs e)
        {
            ExitCustomMouseMode();
            SaveWindowSizeAndLocation();

            ThumbnailFocused?.Invoke(Id);
        }

        private void MouseLeave_Handler(object sender, EventArgs e)
        {
            ThumbnailLostFocus?.Invoke(Id);
        }

        private void MouseDown_Handler(object sender, MouseEventArgs e)
        {
            MouseDownEventHandler(e.Button, ModifierKeys);
        }

        private void MouseMove_Handler(object sender, MouseEventArgs e)
        {
            if (_isCustomMouseModeActive)
            {
                ProcessCustomMouseMode(e.Button.HasFlag(MouseButtons.Left), e.Button.HasFlag(MouseButtons.Right));
            }
        }

        private void MouseUp_Handler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ExitCustomMouseMode();
            }
        }

        private void HotkeyPressed_Handler(object sender, HandledEventArgs e)
        {
            ThumbnailActivated?.Invoke(Id);

            e.Handled = true;
        }

        #endregion

        #region Custom Mouse mode

        // This pair of methods saves/restores certain window properties
        // Methods are used to remove the 'Zoom' effect (if any) when the
        // custom resize/move mode is activated
        // Methods are kept on this level because moving to the presenter
        // the code that responds to the mouse events like movement
        // seems like a huge overkill
        private void SaveWindowSizeAndLocation()
        {
            _baseZoomSize = Size;
            _baseZoomLocation = Location;
            _baseZoomMaximumSize = MaximumSize;
        }

        private void RestoreWindowSizeAndLocation()
        {
            Size = _baseZoomSize;
            MaximumSize = _baseZoomMaximumSize;
            Location = _baseZoomLocation;
        }

        private void EnterCustomMouseMode()
        {
            RestoreWindowSizeAndLocation();

            _isCustomMouseModeActive = true;
            _baseMousePosition = MousePosition;
        }

        private void ProcessCustomMouseMode(bool leftButton, bool rightButton)
        {
            Point mousePosition = MousePosition;
            int offsetX = mousePosition.X - _baseMousePosition.X;
            int offsetY = mousePosition.Y - _baseMousePosition.Y;
            _baseMousePosition = mousePosition;

            // Left + Right buttons trigger thumbnail resize
            // Right button only trigger thumbnail movement
            if (leftButton && rightButton)
            {
                Size = new Size(Size.Width + offsetX, Size.Height + offsetY);
                _baseZoomSize = Size;
            }
            else
            {
                Location = new Point(Location.X + offsetX, Location.Y + offsetY);
                _baseZoomLocation = Location;
            }
        }

        private void ExitCustomMouseMode()
        {
            _isCustomMouseModeActive = false;
        }

        #endregion

        #region Custom GUI events

        protected virtual void MouseDownEventHandler(MouseButtons mouseButtons, Keys modifierKeys)
        {
            switch (mouseButtons)
            {
                case MouseButtons.Left when modifierKeys == Keys.Control:
                    ThumbnailDeactivated?.Invoke(Id, false);
                    break;
                case MouseButtons.Left when modifierKeys == (Keys.Control | Keys.Shift):
                    ThumbnailDeactivated?.Invoke(Id, true);
                    break;
                case MouseButtons.Left:
                    ThumbnailActivated?.Invoke(Id);
                    break;
                case MouseButtons.Right:
                case MouseButtons.Left | MouseButtons.Right:
                    EnterCustomMouseMode();
                    break;
            }
        }

        #endregion
    }
}