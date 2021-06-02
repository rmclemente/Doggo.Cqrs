using System.Collections.Generic;
using System.Net;

namespace Doggo.Infra.CrossCutting.Communication.Messages
{
    public class QueryResponse : Response
    {
        public QueryResponse(IEnumerable<object> result)
        {
            Result = result;
        }

        public QueryResponse(object result)
        {
            Result = result;
            StatusCode = result is null ? HttpStatusCode.NotFound : HttpStatusCode.OK;
        }
    }
}
