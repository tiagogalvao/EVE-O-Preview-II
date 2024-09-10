using System.Drawing;
using System.Windows.Forms;

namespace EveOPreview.View.Custom;

public class BorderedLabel : Label
{
    // Background color for the rectangle
    public Color BackgroundFillColor { get; set; } = Color.Black;

    protected override void OnPaint(PaintEventArgs e)
    {
        // Ensure the base class paint logic is executed
        base.OnPaint(e);

        // Measure the size of the text
        SizeF textSize = e.Graphics.MeasureString(Text, Font);

        // Create a rectangle slightly larger than the text size
        RectangleF rect = new RectangleF(0, 0, textSize.Width - 1, textSize.Height - 1);

        // Draw the rectangle (use any color you like for the background)
        using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(255, 1, 1, 1)))
        {
            e.Graphics.FillRectangle(backgroundBrush, rect);
        }

        // Draw the text on top of the rectangle
        using (Brush textBrush = new SolidBrush(ForeColor))
        {
            e.Graphics.DrawString(Text, Font, textBrush, rect.Location);
        }
    }
}