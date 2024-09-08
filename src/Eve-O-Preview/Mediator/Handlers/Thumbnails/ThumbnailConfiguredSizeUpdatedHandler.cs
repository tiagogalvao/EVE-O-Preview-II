using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Services.Interface;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
    internal sealed class ThumbnailConfiguredSizeUpdatedHandler : INotificationHandler<ThumbnailConfiguredSizeUpdated>
    {
        private readonly IThumbnailManager _manager;

        public ThumbnailConfiguredSizeUpdatedHandler(IThumbnailManager manager)
        {
            _manager = manager;
        }

        public Task Handle(ThumbnailConfiguredSizeUpdated notification, CancellationToken cancellationToken)
        {
            _manager.UpdateThumbnailsSize();

            return Task.CompletedTask;
        }
    }
}