using MediatR;
using System;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public abstract class Command : Message, IRequest<CommandResponse>
    {
        public DateTime Timestamp { get; protected set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
