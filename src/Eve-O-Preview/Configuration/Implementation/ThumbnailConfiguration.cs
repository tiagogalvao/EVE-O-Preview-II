using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Configuration.Interface;
using EveOPreview.Core.Configuration;
using Newtonsoft.Json;

namespace EveOPreview.Configuration.Implementation
{
    internal sealed class ThumbnailConfiguration : IThumbnailConfiguration
    {
        // Constants
        private const int DEFAULT_THUMBNAIL_WIDTH = 384;
        private const int DEFAULT_THUMBNAIL_HEIGHT = 216;
        private const int MIN_THUMBNAIL_WIDTH = 192;
        private const int MIN_THUMBNAIL_HEIGHT = 108;
        private const int MAX_THUMBNAIL_WIDTH = 960;
        private const int MAX_THUMBNAIL_HEIGHT = 540;

        // Fields
        private bool _enablePerClientThumbnailLayouts;
        private bool _enableClientLayoutTracking;

        // Constructor
        public ThumbnailConfiguration()
        {
            PerClientLayout = new Dictionary<string, Dictionary<string, Point>>();
            FlatLayout = new Dictionary<string, Point>();
            ClientLayout = new Dictionary<string, Core.Model.Client.Layout>();
            ClientHotkey = new Dictionary<string, string>();
            DisableThumbnail = new Dictionary<string, bool>();
            PriorityClients = new List<string>();

            MinimizeToTray = false;
            ThumbnailRefreshPeriod = 500;
            EnableCompatibilityMode = false;
            ThumbnailOpacity = 0.5;
            EnableClientLayoutTracking = false;
            HideActiveClientThumbnail = false;
            MinimizeInactiveClients = false;
            ShowThumbnailsAlwaysOnTop = true;
            EnablePerClientThumbnailLayouts = false;
            HideThumbnailsOnLostFocus = false;
            HideThumbnailsDelay = 2; // 2 refresh cycles (1.0 sec)
            ThumbnailSize = new Size(DEFAULT_THUMBNAIL_WIDTH, DEFAULT_THUMBNAIL_HEIGHT);
            ThumbnailMinimumSize = new Size(MIN_THUMBNAIL_WIDTH, MIN_THUMBNAIL_HEIGHT);
            ThumbnailMaximumSize = new Size(MAX_THUMBNAIL_WIDTH, MAX_THUMBNAIL_HEIGHT);
            ThumbnailFontSize = 8;
            EnableThumbnailSnap = true;
            ThumbnailZoomEnabled = false;
            ThumbnailZoomFactor = 2;
            ThumbnailZoomAnchor = ZoomAnchor.NW;
            ShowThumbnailOverlays = true;
            ShowThumbnailFrames = false;
            EnableActiveClientHighlight = false;
            ActiveClientHighlightColor = Color.GreenYellow;
            ActiveClientHighlightThickness = 3;
        }

        // Properties
        public bool MinimizeToTray { get; set; }
        public int ThumbnailRefreshPeriod { get; set; }
        [JsonProperty("CompatibilityMode")]
        public bool EnableCompatibilityMode { get; set; }
        [JsonProperty("ThumbnailsOpacity")]
        public double ThumbnailOpacity { get; set; }
        public bool EnableClientLayoutTracking
        {
            get => _enableClientLayoutTracking;
            set
            {
                if (!value) ClientLayout.Clear();
                _enableClientLayoutTracking = value;
            }
        }
        public bool HideActiveClientThumbnail { get; set; }
        public bool MinimizeInactiveClients { get; set; }
        public bool ShowThumbnailsAlwaysOnTop { get; set; }
        public bool EnablePerClientThumbnailLayouts
        {
            get => _enablePerClientThumbnailLayouts;
            set
            {
                if (!value) PerClientLayout.Clear();
                _enablePerClientThumbnailLayouts = value;
            }
        }
        public bool HideThumbnailsOnLostFocus { get; set; }
        public int HideThumbnailsDelay { get; set; }
        public Size ThumbnailSize { get; set; }
        public Size ThumbnailMaximumSize { get; set; }
        public Size ThumbnailMinimumSize { get; set; }
        public bool EnableThumbnailSnap { get; set; }
        [JsonProperty("EnableThumbnailZoom")]
        public bool ThumbnailZoomEnabled { get; set; }
        public int ThumbnailZoomFactor { get; set; }
        public float ThumbnailFontSize { get; set; }
        public ZoomAnchor ThumbnailZoomAnchor { get; set; }
        public bool ShowThumbnailOverlays { get; set; }
        public bool ShowThumbnailFrames { get; set; }
        public bool EnableActiveClientHighlight { get; set; }
        public Color ActiveClientHighlightColor { get; set; }
        public int ActiveClientHighlightThickness { get; set; }
        [JsonProperty]
        private Dictionary<string, Dictionary<string, Point>> PerClientLayout { get; set; }
        [JsonProperty]
        private Dictionary<string, Point> FlatLayout { get; set; }
        [JsonProperty]
        private Dictionary<string, Core.Model.Client.Layout> ClientLayout { get; set; }
        [JsonProperty]
        private Dictionary<string, string> ClientHotkey { get; set; }
        [JsonProperty]
        private Dictionary<string, bool> DisableThumbnail { get; set; }
        [JsonProperty]
        private List<string> PriorityClients { get; set; }

        // Methods
        public Point GetDefaultThumbnailLocation() => new Point(5, 5);

        public Point GetThumbnailLocation(string currentClient, string activeClient, Point defaultLocation)
        {
            if (EnablePerClientThumbnailLayouts && !string.IsNullOrEmpty(activeClient))
            {
                if (PerClientLayout.TryGetValue(activeClient, out var layoutSource) &&
                    layoutSource.TryGetValue(currentClient, out var location))
                {
                    return location;
                }
            }

            return FlatLayout.TryGetValue(currentClient, out var flatLocation) ? flatLocation : defaultLocation;
        }

        public void SetThumbnailLocation(string currentClient, string activeClient, Point location)
        {
            if (EnablePerClientThumbnailLayouts && !string.IsNullOrEmpty(activeClient))
            {
                if (!PerClientLayout.TryGetValue(activeClient, out var layoutSource))
                {
                    layoutSource = new Dictionary<string, Point>();
                    PerClientLayout[activeClient] = layoutSource;
                }
                layoutSource[currentClient] = location;
            }
            else
            {
                FlatLayout[currentClient] = location;
            }
        }

        public Core.Model.Client.Layout GetClientLayout(string currentClient) =>
            ClientLayout.TryGetValue(currentClient, out var layout) ? layout : null;

        public void SetClientLayout(string currentClient, Core.Model.Client.Layout layout) =>
            ClientLayout[currentClient] = layout;

        public Keys GetClientHotkey(string currentClient)
        {
            if (ClientHotkey.TryGetValue(currentClient, out var hotkey))
            {
                var rawValue = new KeysConverter().ConvertFromInvariantString(hotkey);
                return rawValue is Keys key ? key : Keys.None;
            }
            return Keys.None;
        }

        public void SetClientHotkey(string currentClient, Keys hotkey) =>
            ClientHotkey[currentClient] = new KeysConverter().ConvertToInvariantString(hotkey);

        public bool IsPriorityClient(string currentClient) => PriorityClients.Contains(currentClient);

        public bool IsThumbnailDisabled(string currentClient) =>
            DisableThumbnail.TryGetValue(currentClient, out var isDisabled) && isDisabled;

        public void ToggleThumbnail(string currentClient, bool isDisabled) =>
            DisableThumbnail[currentClient] = isDisabled;

        public void ApplyRestrictions()
        {
            ThumbnailRefreshPeriod = ApplyRestrictions(ThumbnailRefreshPeriod, 300, 1000);
            ThumbnailSize = new Size(
                ApplyRestrictions(ThumbnailSize.Width, ThumbnailMinimumSize.Width, ThumbnailMaximumSize.Width),
                ApplyRestrictions(ThumbnailSize.Height, ThumbnailMinimumSize.Height, ThumbnailMaximumSize.Height)
            );
            ThumbnailOpacity = ApplyRestrictions((int)(ThumbnailOpacity * 100.0), 20, 100) / 100.0;
            ThumbnailZoomFactor = ApplyRestrictions(ThumbnailZoomFactor, 2, 10);
            ActiveClientHighlightThickness = ApplyRestrictions(ActiveClientHighlightThickness, 1, 6);
        }

        private static int ApplyRestrictions(int value, int minimum, int maximum) =>
            value < minimum ? minimum : value > maximum ? maximum : value;
    }
}
