using Doggo.Application.Notifications;
using Doggo.Infra.CrossCutting.Communication;
using Doggo.Infra.CrossCutting.Communication.Messages;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doggo.Application.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : CommandResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IMediatorHandler _mediatorHandler;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IMediatorHandler mediatorHandler)
        {
            _validators = validators;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(request => request.Validate(context))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(failure => failure != null)
                .ToList();

            if (!failures.Any())
                return await next();

            var commandRequest = request as Command;
            await PublishDomainNotifications(failures, commandRequest.AggregateId);

            return new CommandResponse() as TResponse;
        }

        private async Task PublishDomainNotifications(IEnumerable<ValidationFailure> failures, Guid aggregateId)
        {
            foreach (var failure in failures)
                await _mediatorHandler.PublishNotification(new DomainNotification(failure.PropertyName, failure.ErrorMessage));
        }
    }
}
