using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Presenters.Interface;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
    internal sealed class ThumbnailListUpdatedHandler : INotificationHandler<ThumbnailListUpdated>
    {
        #region Private fields

        private readonly IMainFormPresenter _presenter;

        #endregion

        public ThumbnailListUpdatedHandler(IMainFormPresenter presenter)
        {
            _presenter = presenter;
        }

        public async Task Handle(ThumbnailListUpdated notification, CancellationToken cancellationToken)
        {
            if (notification.Added.Count > 0)
            {
                _presenter.AddThumbnails(notification.Added);
            }

            if (notification.Removed.Count > 0)
            {
                _presenter.RemoveThumbnails(notification.Removed);
            }
            
            // Await any potential asynchronous operations in the future
            await Task.CompletedTask;
        }
    }
}