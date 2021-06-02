using Doggo.Application.Notifications;
using Doggo.Infra.CrossCutting.Communication.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Doggo.Api.Controllers
{
    [ApiController]
    public abstract class CustomControllerBase : ControllerBase
    {
        private readonly DomainNotificationContext _notificationContext;

        protected CustomControllerBase(DomainNotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        protected string RequestPath => Request.HttpContext.Request.Path.Value;

        /// <summary>
        /// Returns a response with a HTTP status code and a complex Api Response Object.
        /// Implementation of RFC 7807, Problem Details for HTTP APIs, for Bad Requests.
        /// Reference: https://tools.ietf.org/html/rfc7807
        /// </summary>
        /// <param name="response">CommandResponse</param>
        /// <returns>
        ///     BadRequest (400): ValidationProblemDetails;<br />
        ///     Created (201): Location header;<br />
        ///     NoContent (204): No Content;<br />
        ///     NotFound (404): No Content;<br />
        ///     Success (200): Object Response;
        /// </returns>
        protected IActionResult CommandResponseHandler(CommandResponse response)
        {
            if (_notificationContext.HasDomainNotification)
                return BadRequest(GetProblemDetailsResponse(_notificationContext.DomainNotifications));

            return response.StatusCode switch
            {
                HttpStatusCode.Created => Created($"{RequestPath}/{response?.Result}", null),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(),
                _ => Ok(response.Result),
            };
        }

        protected IActionResult QueryResponseHandler(QueryResponse response)
        {
            return response.StatusCode == HttpStatusCode.NotFound ? NotFound() : Ok(response.Result);
        }

        private static ValidationProblemDetails GetProblemDetailsResponse(IEnumerable<DomainNotification> notifications)
        {
            var result = new Dictionary<string, string[]>();

            foreach (var notification in notifications)
            {
                if (!result.ContainsKey(notification.Key))
                    result.Add(notification.Key, notifications.Where(p => p.Key == notification.Key)
                        .Select(p => p.Value)
                        .ToArray());
            }

            return new ValidationProblemDetails(result);
        }
    }
}
