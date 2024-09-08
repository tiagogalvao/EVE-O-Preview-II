using System.Drawing;
using EveOPreview.Services.Interface;

namespace EveOPreview.View.Implementation
{
    internal sealed class LiveThumbnailView : ThumbnailView
    {
        #region Private fields

        private IDwmThumbnail _thumbnail;
        private Point _startLocation;
        private Point _endLocation;

        #endregion

        public LiveThumbnailView(IWindowManager windowManager) : base(windowManager)
        {
            _startLocation = new Point(0, 0);
            _endLocation = new Point(ClientSize);
        }

        protected override void RefreshThumbnail(bool forceRefresh)
        {
            // To prevent flickering the old broken thumbnail is removed AFTER the new shiny one is created
            IDwmThumbnail obsoleteThumbnail = forceRefresh ? _thumbnail : null;
            if ((_thumbnail == null) || forceRefresh)
            {
                RegisterThumbnail();
            }

            obsoleteThumbnail?.Unregister();
        }

        protected override void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft)
        {
            var left = 0 + highlightWidthLeft;
            var top = 0 + highlightWidthTop;
            var right = baseWidth - highlightWidthRight;
            var bottom = baseHeight - highlightWidthBottom;

            if ((_startLocation.X == left) && (_startLocation.Y == top) && (_endLocation.X == right) && (_endLocation.Y == bottom))
            {
                return; // No update required
            }

            _startLocation = new Point(left, top);
            _endLocation = new Point(right, bottom);

            _thumbnail.Move(left, top, right, bottom);
            _thumbnail.Update();
        }

        private void RegisterThumbnail()
        {
            _thumbnail = WindowManager.GetLiveThumbnail(Handle, Id);
            _thumbnail.Move(_startLocation.X, _startLocation.Y, _endLocation.X, _endLocation.Y);
            _thumbnail.Update();
        }
    }
}