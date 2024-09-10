using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Services.Interface;

namespace EveOPreview.View.Implementation
{
    internal sealed class StaticThumbnailView : ThumbnailView
    {
        private readonly PictureBox _thumbnail;

        public StaticThumbnailView(IWindowManager windowManager) : base(windowManager)
        {
            _thumbnail = new StaticThumbnailImage
            {
                TabStop = false,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, ClientSize.Height)
            };
            Controls.Add(_thumbnail);
        }

        protected override void RefreshThumbnail(bool forceRefresh)
        {
            if (!forceRefresh) return;

            var thumbnail = WindowManager.GetStaticThumbnail(Id);
            if (thumbnail is not null)
            {
                var oldImage = _thumbnail.Image;
                _thumbnail.Image = thumbnail;
                oldImage?.Dispose();
            }
        }

        protected override void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft)
        {
            var left = highlightWidthLeft;
            var top = highlightWidthTop;

            if (IsLocationUpdateRequired(_thumbnail.Location, left, top))
                _thumbnail.Location = new Point(left, top);

            var width = baseWidth - highlightWidthLeft - highlightWidthRight;
            var height = baseHeight - highlightWidthTop - highlightWidthBottom;

            if (IsSizeUpdateRequired(_thumbnail.Size, width, height))
                _thumbnail.Size = new Size(width, height);
        }

        private bool IsLocationUpdateRequired(Point currentLocation, int left, int top) =>
            currentLocation.X != left || currentLocation.Y != top;

        private bool IsSizeUpdateRequired(Size currentSize, int width, int height) =>
            currentSize.Width != width || currentSize.Height != height;
    }
}
