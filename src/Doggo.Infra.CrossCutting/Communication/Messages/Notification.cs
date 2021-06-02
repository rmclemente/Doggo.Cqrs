using MediatR;
using System;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public abstract class Notification : Message, INotification
    {
        public string Key { get; protected set; }
        public string Value { get; protected set; }
        public DateTime Timestamp { get; protected set; }

        protected Notification(string key, string value)
        {
            Timestamp = DateTime.Now;
            Key = key;
            Value = value;
        }
    }
}
