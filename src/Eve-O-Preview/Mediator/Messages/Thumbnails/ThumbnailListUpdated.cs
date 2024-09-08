using System.Collections.Generic;
using MediatR;

namespace EveOPreview.Mediator.Messages.Thumbnails
{
    internal sealed class ThumbnailListUpdated : INotification
    {
        public ThumbnailListUpdated(IList<string> addedThumbnails, IList<string> removedThumbnails)
        {
            Added = addedThumbnails;
            Removed = removedThumbnails;
        }

        public IList<string> Added { get; }
        public IList<string> Removed { get; }
    }
}