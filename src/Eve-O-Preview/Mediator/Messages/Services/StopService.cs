using MediatR;

namespace EveOPreview.Mediator.Messages.Services
{
	internal sealed class StopService : IRequest, IRequest<Unit>
	{
	}
}