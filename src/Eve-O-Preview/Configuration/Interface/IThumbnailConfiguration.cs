using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Core.Configuration;

namespace EveOPreview.Configuration.Interface
{
    public interface IThumbnailConfiguration
    {
        bool MinimizeToTray { get; set; }
        int ThumbnailRefreshPeriod { get; set; }

        bool EnableCompatibilityMode { get; set; }

        double ThumbnailOpacity { get; set; }

        bool EnableClientLayoutTracking { get; set; }
        bool HideActiveClientThumbnail { get; set; }
        bool MinimizeInactiveClients { get; set; }
        bool ShowThumbnailsAlwaysOnTop { get; set; }
        bool EnablePerClientThumbnailLayouts { get; set; }

        bool HideThumbnailsOnLostFocus { get; set; }
        int HideThumbnailsDelay { get; set; }

        Size ThumbnailSize { get; set; }
        Size ThumbnailMinimumSize { get; set; }
        Size ThumbnailMaximumSize { get; set; }
        
        float ThumbnailFontSize { get; set; }

        bool EnableThumbnailSnap { get; set; }

        bool ThumbnailZoomEnabled { get; set; }
        int ThumbnailZoomFactor { get; set; }
        ZoomAnchor ThumbnailZoomAnchor { get; set; }

        bool ShowThumbnailOverlays { get; set; }
        bool ShowThumbnailFrames { get; set; }

        bool EnableActiveClientHighlight { get; set; }
        Color ActiveClientHighlightColor { get; set; }
        int ActiveClientHighlightThickness { get; set; }

        Point GetDefaultThumbnailLocation();
        Point GetThumbnailLocation(string currentClient, string activeClient, Point defaultLocation);
        void SetThumbnailLocation(string currentClient, string activeClient, Point location);

        Core.Model.Client.Layout GetClientLayout(string currentClient);
        void SetClientLayout(string currentClient, Core.Model.Client.Layout layout);

        Keys GetClientHotkey(string currentClient);
        void SetClientHotkey(string currentClient, Keys hotkey);

        bool IsPriorityClient(string currentClient);

        bool IsThumbnailDisabled(string currentClient);
        void ToggleThumbnail(string currentClient, bool isDisabled);

        void ApplyRestrictions();
    }
}