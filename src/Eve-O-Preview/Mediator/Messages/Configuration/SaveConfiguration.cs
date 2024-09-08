using MediatR;

namespace EveOPreview.Mediator.Messages.Configuration
{
	internal sealed class SaveConfiguration : IRequest, IRequest<Unit>
	{
	}
}