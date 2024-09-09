using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Presenters.Interface;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
    internal sealed class ThumbnailActiveSizeUpdatedHandler : INotificationHandler<ThumbnailActiveSizeUpdated>
    {
        private readonly IMainFormPresenter _presenter;

        public ThumbnailActiveSizeUpdatedHandler(IMainFormPresenter presenter)
        {
            _presenter = presenter;
        }

        public Task Handle(ThumbnailActiveSizeUpdated notification, CancellationToken cancellationToken)
        {
            _presenter.UpdateThumbnailSize(notification.Value);

            return Task.CompletedTask;
        }
    }
}