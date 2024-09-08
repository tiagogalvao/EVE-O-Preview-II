using System.Drawing;
using EveOPreview.Mediator.Messages.Base;

namespace EveOPreview.Mediator.Messages.Thumbnails
{
    sealed class ThumbnailActiveSizeUpdated : NotificationBase<Size>
    {
        public ThumbnailActiveSizeUpdated(Size size) : base(size)
        {
        }
    }
}