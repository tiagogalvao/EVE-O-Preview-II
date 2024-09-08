using MediatR;

namespace EveOPreview.Mediator.Messages.Base
{
    internal abstract class NotificationBase<TValue> : INotification
    {
        protected NotificationBase(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; }
    }
}