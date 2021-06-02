using MediatR;
using System;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
