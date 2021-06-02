using System.Collections.Generic;
using System.Linq;

namespace Doggo.Application.Notifications
{
    public class DomainNotificationContext
    {
        private List<DomainNotification> _domainNotifications;
        public IReadOnlyCollection<DomainNotification> DomainNotifications => _domainNotifications;
        public bool HasDomainNotification => _domainNotifications.Any();

        public DomainNotificationContext()
        {
            _domainNotifications = new List<DomainNotification>();
        }

        public void AddNotification(DomainNotification notification)
        {
            _domainNotifications.Add(notification);
        }

        public void Dispose() => _domainNotifications = new List<DomainNotification>();
    }
}
