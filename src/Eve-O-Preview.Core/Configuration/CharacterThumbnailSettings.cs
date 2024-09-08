using System.Drawing;

namespace EveOPreview.Core.Configuration;

public class CharacterThumbnailSettings
{
    public bool ShowHighlight { get; set; }
    public Color BackgroundColor { get; set; }
    public Color BorderColor { get; set; }
    
    public double Opacity { get; set; }
    
    public int Width { get; set; }
    public int Heigth { get; set; }
    
    public int FontSize { get; set; }
    
    public bool ZoomOnHover { get; set; }
    public double ZoomFactor { get; set; }
    public ZoomAnchor ZoomAnchor { get; set; }
}