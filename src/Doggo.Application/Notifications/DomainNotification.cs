using Doggo.Infra.CrossCutting.Communication.Messages;
using System;

namespace Doggo.Application.Notifications
{
    public class DomainNotification : Notification
    {
        public Guid DomainNotificationId { get; private set; }

        public DomainNotification(string key, string value) : base(key, value)
        {
            DomainNotificationId = Guid.NewGuid();
        }

        public DomainNotification(string key, string value, Guid aggregateId) : base(key, value)
        {
            DomainNotificationId = Guid.NewGuid();
            AggregateId = aggregateId;
        }
    }
}
