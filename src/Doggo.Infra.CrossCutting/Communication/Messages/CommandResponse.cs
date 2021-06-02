using System.Net;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public class CommandResponse : Response
    {
        public CommandResponse() { }

        public CommandResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public CommandResponse(object result, HttpStatusCode statusCode)
        {
            Result = result;
            StatusCode = statusCode;
        }
    }
}
