using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Core.Action;
using EveOPreview.Core.Configuration;
using EveOPreview.View.Interface;

namespace EveOPreview.View.Implementation
{
    public partial class MainForm : Form, IMainFormView
    {
        #region Private fields

        private readonly Dictionary<ZoomAnchor, RadioButton> _zoomAnchorMap = new Dictionary<ZoomAnchor, RadioButton>();
        private ZoomAnchor _cachedThumbnailZoomAnchor;
        private bool _suppressEvents;
        private Size _minimumSize;
        private Size _maximumSize;

        #endregion

        public MainForm()
        {
            _cachedThumbnailZoomAnchor = ZoomAnchor.NW;
            _suppressEvents = false;
            _minimumSize = new Size(80, 60);
            _maximumSize = new Size(80, 60);

            InitializeComponent();

            ThumbnailsList.DisplayMember = "Title";

            InitZoomAnchorMap();

            if (!MinimizeToTray)
            {
                RestoreForm();
            }
        }

        public bool MinimizeToTray
        {
            get => MinimizeToTrayCheckBox.Checked;
            set => MinimizeToTrayCheckBox.Checked = value;
        }

        public double ThumbnailOpacity
        {
            get => Math.Min(ThumbnailOpacityTrackBar.Value / 100.00, 1.00);
            set
            {
                int barValue = (int)(100.0 * value);
                if (barValue > 100)
                {
                    barValue = 100;
                }
                else if (barValue < 10)
                {
                    barValue = 10;
                }

                ThumbnailOpacityTrackBar.Value = barValue;
            }
        }

        public bool EnableClientLayoutTracking
        {
            get => EnableClientLayoutTrackingCheckBox.Checked;
            set => EnableClientLayoutTrackingCheckBox.Checked = value;
        }

        public bool HideActiveClientThumbnail
        {
            get => HideActiveClientThumbnailCheckBox.Checked;
            set => HideActiveClientThumbnailCheckBox.Checked = value;
        }

        public bool MinimizeInactiveClients
        {
            get => MinimizeInactiveClientsCheckBox.Checked;
            set => MinimizeInactiveClientsCheckBox.Checked = value;
        }

        public bool ShowThumbnailsAlwaysOnTop
        {
            get => ShowThumbnailsAlwaysOnTopCheckBox.Checked;
            set => ShowThumbnailsAlwaysOnTopCheckBox.Checked = value;
        }

        public bool HideThumbnailsOnLostFocus
        {
            get => HideThumbnailsOnLostFocusCheckBox.Checked;
            set => HideThumbnailsOnLostFocusCheckBox.Checked = value;
        }

        public bool EnablePerClientThumbnailLayouts
        {
            get => EnablePerClientThumbnailsLayoutsCheckBox.Checked;
            set => EnablePerClientThumbnailsLayoutsCheckBox.Checked = value;
        }

        public Size ThumbnailSize
        {
            get => new Size((int)ThumbnailsWidthNumericEdit.Value, (int)ThumbnailsHeightNumericEdit.Value);
            set
            {
                ThumbnailsWidthNumericEdit.Value = value.Width;
                ThumbnailsHeightNumericEdit.Value = value.Height;
            }
        }

        public float ThumbnailFontSize
        {
            get => (float)ThumbnailsFontSizeNumericEdit.Value;
            set => ThumbnailsFontSizeNumericEdit.Value = (decimal)value;
        }

        public bool EnableThumbnailZoom
        {
            get => EnableThumbnailZoomCheckBox.Checked;
            set
            {
                EnableThumbnailZoomCheckBox.Checked = value;
                RefreshZoomSettings();
            }
        }

        public int ThumbnailZoomFactor
        {
            get => (int)ThumbnailZoomFactorNumericEdit.Value;
            set => ThumbnailZoomFactorNumericEdit.Value = value;
        }

        public ZoomAnchor ThumbnailZoomAnchor
        {
            get
            {
                if (_zoomAnchorMap[_cachedThumbnailZoomAnchor].Checked)
                {
                    return _cachedThumbnailZoomAnchor;
                }

                foreach (KeyValuePair<ZoomAnchor, RadioButton> valuePair in _zoomAnchorMap)
                {
                    if (!valuePair.Value.Checked)
                    {
                        continue;
                    }

                    _cachedThumbnailZoomAnchor = valuePair.Key;
                    return _cachedThumbnailZoomAnchor;
                }

                // Default value
                return ZoomAnchor.NW;
            }
            set
            {
                _cachedThumbnailZoomAnchor = value;
                _zoomAnchorMap[_cachedThumbnailZoomAnchor].Checked = true;
            }
        }

        public bool ShowThumbnailOverlays
        {
            get => ShowThumbnailOverlaysCheckBox.Checked;
            set => ShowThumbnailOverlaysCheckBox.Checked = value;
        }

        public bool ShowThumbnailFrames
        {
            get => ShowThumbnailFramesCheckBox.Checked;
            set => ShowThumbnailFramesCheckBox.Checked = value;
        }

        public bool EnableActiveClientHighlight
        {
            get => EnableActiveClientHighlightCheckBox.Checked;
            set => EnableActiveClientHighlightCheckBox.Checked = value;
        }

        public Color ActiveClientHighlightColor
        {
            get => _activeClientHighlightColor;
            set
            {
                _activeClientHighlightColor = value;
                ActiveClientHighlightColorButton.BackColor = value;
            }
        }

        private Color _activeClientHighlightColor;

        public new void Show()
        {
            _suppressEvents = true;
            FormActivated?.Invoke();
            _suppressEvents = false;

            Application.Run();
        }

        public void SetThumbnailSizeLimitations(Size minimumSize, Size maximumSize)
        {
            _minimumSize = minimumSize;
            _maximumSize = maximumSize;
        }

        public void Minimize()
        {
            WindowState = FormWindowState.Minimized;
        }

        public void SetVersionInfo(string version)
        {
            VersionLabel.Text = version;
        }

        public void SetDocumentationUrl(string url)
        {
            DocumentationLink.Text = url;
        }

        public void AddThumbnails(IList<IThumbnailDescription> thumbnails)
        {
            ThumbnailsList.BeginUpdate();

            foreach (IThumbnailDescription view in thumbnails)
            {
                ThumbnailsList.SetItemChecked(ThumbnailsList.Items.Add(view), view.IsDisabled);
            }

            ThumbnailsList.EndUpdate();
        }

        public void RemoveThumbnails(IList<IThumbnailDescription> thumbnails)
        {
            ThumbnailsList.BeginUpdate();

            foreach (IThumbnailDescription view in thumbnails)
            {
                ThumbnailsList.Items.Remove(view);
            }

            ThumbnailsList.EndUpdate();
        }

        public void RefreshZoomSettings()
        {
            bool enableControls = EnableThumbnailZoom;
            ThumbnailZoomFactorNumericEdit.Enabled = enableControls;
            ZoomAnchorPanel.Enabled = enableControls;
        }

        public void RestoreForm()
        {
            base.Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        public Action ApplicationExitRequested { get; set; }

        public Action FormActivated { get; set; }

        public Action FormMinimized { get; set; }

        public Action<ViewCloseRequest> FormCloseRequested { get; set; }

        public Action ApplicationSettingsChanged { get; set; }

        public Action ThumbnailsSizeChanged { get; set; }

        public Action<string> ThumbnailStateChanged { get; set; }

        public Action DocumentationLinkActivated { get; set; }

        #region UI events

        private void ContentTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl control = (TabControl)sender;
            TabPage page = control.TabPages[e.Index];
            Rectangle bounds = control.GetTabRect(e.Index);

            Graphics graphics = e.Graphics;

            Brush textBrush = new SolidBrush(SystemColors.ActiveCaptionText);
            Brush backgroundBrush = (e.State == DrawItemState.Selected)
                ? new SolidBrush(SystemColors.Control)
                : new SolidBrush(SystemColors.ControlDark);
            graphics.FillRectangle(backgroundBrush, e.Bounds);

            // Use our own font
            Font font = new Font("Arial", Font.Size * 1.5f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string and center the text
            StringFormat stringFlags = new StringFormat();
            stringFlags.Alignment = StringAlignment.Center;
            stringFlags.LineAlignment = StringAlignment.Center;

            graphics.DrawString(page.Text, font, textBrush, bounds, stringFlags);
        }

        private void OptionChanged_Handler(object sender, EventArgs e)
        {
            if (_suppressEvents)
            {
                return;
            }

            ApplicationSettingsChanged?.Invoke();
        }

        private void ThumbnailSizeChanged_Handler(object sender, EventArgs e)
        {
            if (_suppressEvents)
            {
                return;
            }

            // Perform some View work that is not properly done in the Control
            _suppressEvents = true;
            Size thumbnailSize = ThumbnailSize;
            thumbnailSize.Width = Math.Min(Math.Max(thumbnailSize.Width, _minimumSize.Width), _maximumSize.Width);
            thumbnailSize.Height = Math.Min(Math.Max(thumbnailSize.Height, _minimumSize.Height), _maximumSize.Height);
            ThumbnailSize = thumbnailSize;
            _suppressEvents = false;

            ThumbnailsSizeChanged?.Invoke();
        }

        private void ActiveClientHighlightColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = ActiveClientHighlightColor;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                ActiveClientHighlightColor = dialog.Color;
            }

            OptionChanged_Handler(sender, e);
        }

        private void ThumbnailsList_ItemCheck_Handler(object sender, ItemCheckEventArgs e)
        {
            if (!(ThumbnailsList.Items[e.Index] is IThumbnailDescription selectedItem))
            {
                return;
            }

            selectedItem.IsDisabled = (e.NewValue == CheckState.Checked);

            ThumbnailStateChanged?.Invoke(selectedItem.Title);
        }

        private void DocumentationLinkClicked_Handler(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DocumentationLinkActivated?.Invoke();
        }

        private void MainFormResize_Handler(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                return;
            }

            FormMinimized?.Invoke();
        }

        private void MainFormClosing_Handler(object sender, FormClosingEventArgs e)
        {
            ViewCloseRequest request = new ViewCloseRequest();

            FormCloseRequested?.Invoke(request);

            e.Cancel = !request.Allow;
        }

        private void RestoreMainForm_Handler(object sender, EventArgs e)
        {
            RestoreForm();
        }

        private void ExitMenuItemClick_Handler(object sender, EventArgs e)
        {
            ApplicationExitRequested?.Invoke();
        }

        #endregion

        private void InitZoomAnchorMap()
        {
            _zoomAnchorMap[ZoomAnchor.NW] = ZoomAanchorNWRadioButton;
            _zoomAnchorMap[ZoomAnchor.N] = ZoomAanchorNRadioButton;
            _zoomAnchorMap[ZoomAnchor.NE] = ZoomAanchorNERadioButton;
            _zoomAnchorMap[ZoomAnchor.W] = ZoomAanchorWRadioButton;
            _zoomAnchorMap[ZoomAnchor.C] = ZoomAanchorCRadioButton;
            _zoomAnchorMap[ZoomAnchor.E] = ZoomAanchorERadioButton;
            _zoomAnchorMap[ZoomAnchor.SW] = ZoomAanchorSWRadioButton;
            _zoomAnchorMap[ZoomAnchor.S] = ZoomAanchorSRadioButton;
            _zoomAnchorMap[ZoomAnchor.SE] = ZoomAanchorSERadioButton;
        }
    }
}