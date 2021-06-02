using System.Net;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public abstract class Response
    {
        public object Result { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; }

        public void SetStatus(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
