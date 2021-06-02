using MediatR;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public abstract class Query : IRequest<QueryResponse>
    {
    }
}
