using System.Drawing;
using MediatR;

namespace EveOPreview.Mediator.Messages.Thumbnails
{
    sealed class ThumbnailLocationUpdated : INotification
    {
        public ThumbnailLocationUpdated(string thumbnailName, string activeClientName, Point location)
        {
            ThumbnailName = thumbnailName;
            ActiveClientName = activeClientName;
            Location = location;
        }

        public string ThumbnailName { get; }
        public string ActiveClientName { get; }
        public Point Location { get; }
    }
}