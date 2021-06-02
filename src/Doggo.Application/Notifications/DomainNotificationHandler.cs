using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Doggo.Application.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private readonly DomainNotificationContext _notificationContext;

        public DomainNotificationHandler(DomainNotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notificationContext.AddNotification(notification);
            return Task.CompletedTask;
        }
    }
}
