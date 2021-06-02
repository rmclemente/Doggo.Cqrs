using Doggo.Infra.CrossCutting.Communication.Messages;
using MediatR;
using System.Threading.Tasks;

namespace Doggo.Infra.CrossCutting.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishNotification<TNotification>(TNotification notification) where TNotification : Notification
        {
            await _mediator.Publish(notification);
        }

        public async Task PublishEvent<TEvent>(TEvent eventSource) where TEvent : Event
        {
            await _mediator.Publish(eventSource);
        }

        public async Task<CommandResponse> SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            return await _mediator.Send(command);
        }

        public async Task<QueryResponse> SendQuery<TQuery>(TQuery query) where TQuery : Query
        {
            return await _mediator.Send(query);
        }
    }
}
