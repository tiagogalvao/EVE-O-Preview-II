using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages.Services;
using EveOPreview.Services.Interface;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Services
{
    internal sealed class StartStopServiceHandler : IRequestHandler<StartService>, IRequestHandler<StopService>
    {
        private readonly IThumbnailManager _manager;

        public StartStopServiceHandler(IThumbnailManager manager)
        {
            _manager = manager;
        }

        public Task Handle(StartService message, CancellationToken cancellationToken)
        {
            _manager.Start();

            return Unit.Task;
        }

        public Task Handle(StopService message, CancellationToken cancellationToken)
        {
            _manager.Stop();

            return Unit.Task;
        }
    }
}