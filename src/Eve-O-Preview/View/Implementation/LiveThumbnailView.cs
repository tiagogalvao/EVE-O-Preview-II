using System.Drawing;
using EveOPreview.Services.Interface;

namespace EveOPreview.View.Implementation
{
    internal sealed class LiveThumbnailView : ThumbnailView
    {
        #region Private fields

        private IDwmThumbnail _thumbnail;
        private Rectangle _thumbnailRect;

        #endregion

        public LiveThumbnailView(IWindowManager windowManager) : base(windowManager)
        {
            _thumbnailRect = new Rectangle(Point.Empty, ClientSize);
        }

        protected override void RefreshThumbnail(bool forceRefresh)
        {
            // Remove the old thumbnail only if it's being replaced or refreshed
            if (_thumbnail != null && !forceRefresh)
            {
                return;
            }

            // Register a new thumbnail
            RegisterThumbnail();
        }

        protected override void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft)
        {
            var newRect = new Rectangle(highlightWidthLeft, highlightWidthTop, baseWidth - highlightWidthLeft - highlightWidthRight, baseHeight - highlightWidthTop - highlightWidthBottom);
            if (_thumbnailRect.Equals(newRect))
            {
                return; // No update required
            }

            _thumbnailRect = newRect;
            _thumbnail.Move(newRect.X, newRect.Y, newRect.Right, newRect.Bottom);
            _thumbnail.Update();
        }

        private void RegisterThumbnail()
        {
            _thumbnail?.Unregister(); // Ensure the previous thumbnail is unregistered

            _thumbnail = WindowManager.GetLiveThumbnail(Handle, Id);
            if (_thumbnail == null)
            {
                // Handle the case where the thumbnail could not be created
                return;
            }

            _thumbnail.Move(_thumbnailRect.X, _thumbnailRect.Y, _thumbnailRect.Right, _thumbnailRect.Bottom);
            _thumbnail.Update();
        }
    }
}