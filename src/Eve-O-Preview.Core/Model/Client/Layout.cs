namespace EveOPreview.Core.Model.Client
{
    public class Layout
    {
        public Location Location { get; }
        public Size Size { get; }
        public bool IsMaximized { get; }

        public Layout(int x, int y, int width, int height, bool isMaximized)
        {
            IsMaximized = isMaximized;
            Location = new Location(x, y);
            Size = new Size(width, height);
        }

        public Layout(Location location, Size size, bool isMaximized = false)
        {
            IsMaximized = isMaximized;
            Location = location;
            Size = size;
        }
    }
}