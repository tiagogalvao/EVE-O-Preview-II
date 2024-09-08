using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Presenters.Implementation;
using EveOPreview.Presenters.Interface;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
    internal sealed class ThumbnailListUpdatedHandler : INotificationHandler<ThumbnailListUpdated>
    {
        #region Private fields

        private readonly IMainFormPresenter _presenter;

        #endregion

        public ThumbnailListUpdatedHandler(MainFormPresenter presenter)
        {
            _presenter = presenter;
        }

        public Task Handle(ThumbnailListUpdated notification, CancellationToken cancellationToken)
        {
            if (notification.Added.Count > 0)
            {
                _presenter.AddThumbnails(notification.Added);
            }

            if (notification.Removed.Count > 0)
            {
                _presenter.RemoveThumbnails(notification.Removed);
            }

            return Task.CompletedTask;
        }
    }
}