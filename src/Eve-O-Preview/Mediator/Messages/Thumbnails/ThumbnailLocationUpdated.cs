using System.Drawing;
using MediatR;

namespace EveOPreview.Mediator.Messages.Thumbnails
{
    internal sealed record ThumbnailLocationUpdated(
        string ThumbnailName,
        string ActiveClientName,
        Point Location) : INotification;
}