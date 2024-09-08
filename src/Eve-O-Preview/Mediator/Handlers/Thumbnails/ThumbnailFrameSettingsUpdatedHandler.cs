using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Services.Interface;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
    internal sealed class ThumbnailFrameSettingsUpdatedHandler : INotificationHandler<ThumbnailFrameSettingsUpdated>
    {
        private readonly IThumbnailManager _manager;

        public ThumbnailFrameSettingsUpdatedHandler(IThumbnailManager manager)
        {
            _manager = manager;
        }

        public Task Handle(ThumbnailFrameSettingsUpdated notification, CancellationToken cancellationToken)
        {
            _manager.UpdateThumbnailFrames();

            return Task.CompletedTask;
        }
    }
}