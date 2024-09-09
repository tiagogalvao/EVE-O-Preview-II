using MediatR;

namespace EveOPreview.Mediator.Messages.Thumbnails
{
    internal sealed record ThumbnailFrameSettingsUpdated() : INotification;
}