using MediatR;

namespace EveOPreview.Mediator.Messages.Services
{
    internal sealed class StartService : IRequest, IRequest<Unit>
    {
    }
}