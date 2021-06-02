using Doggo.Application.Commands.Breeds;
using Doggo.Application.Dtos;
using Doggo.Application.Notifications;
using Doggo.Application.Queries.Breeds;
using Doggo.Infra.CrossCutting.Communication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Doggo.Api.Controllers
{
    [Route("api/[controller]")]
    public class BreedController : CustomControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        //private readonly ILogger<BreedController> _logger;

        public BreedController(IMediatorHandler mediatorHandler, DomainNotificationContext notificationContext) : base(notificationContext)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<BreedResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var response = await _mediatorHandler.SendQuery(new GetAllBreedsQuery());
            return QueryResponseHandler(response);
        }

        [HttpGet("{uniqueId:Guid}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BreedResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid uniqueId)
        {
            var response = await _mediatorHandler.SendQuery(new GetBreedByUniqueIdQuery { UniqueId = uniqueId });
            return QueryResponseHandler(response);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateBreedRequest request)
        {
            var command = new CreateBreedCommand(request.Name, request.Type, request.Family, request.Origin, request.DateOfOrigin, request.OtherNames);
            var response = await _mediatorHandler.SendCommand(command);
            return CommandResponseHandler(response);
        }

        [HttpPut("{uniqueId:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromRoute] Guid uniqueId, [FromBody] UpdateBreedRequest request)
        {
            var command = new UpdateBreedCommand(uniqueId, request.Name, request.Type, request.Family, request.Origin, request.DateOfOrigin, request.OtherNames);
            var response = await _mediatorHandler.SendCommand(command);
            return CommandResponseHandler(response);
        }
    }
}
