using Doggo.Infra.CrossCutting.Communication.Messages;
using System.Threading.Tasks;

namespace Doggo.Infra.CrossCutting.Communication
{
    public interface IMediatorHandler
    {
        Task PublishNotification<TNotification>(TNotification notification) where TNotification : Notification;
        Task PublishEvent<TEvent>(TEvent eventSource) where TEvent : Event;
        Task<CommandResponse> SendCommand<TCommand>(TCommand command) where TCommand : Command;
        Task<QueryResponse> SendQuery<TQuery>(TQuery query) where TQuery : Query;
    }
}
