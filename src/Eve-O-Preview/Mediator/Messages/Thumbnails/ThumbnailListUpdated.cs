using System.Collections.Generic;
using MediatR;

namespace EveOPreview.Mediator.Messages.Thumbnails
{
    internal sealed record ThumbnailListUpdated(
        IReadOnlyList<string> Added,
        IReadOnlyList<string> Removed) : INotification;
}