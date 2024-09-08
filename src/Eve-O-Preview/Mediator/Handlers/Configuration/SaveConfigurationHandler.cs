using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Core.Configuration.Interface;
using EveOPreview.Mediator.Messages.Configuration;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Configuration
{
    internal sealed class SaveConfigurationHandler : IRequestHandler<SaveConfiguration>
    {
        private readonly IStorage _storage;

        public SaveConfigurationHandler(IStorage storage)
        {
            _storage = storage;
        }

        public Task Handle(SaveConfiguration message, CancellationToken cancellationToken)
        {
            _storage.Save();

            return Unit.Task;
        }
    }
}