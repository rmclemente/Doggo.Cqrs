using Doggo.Application.Notifications;
using Doggo.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Application.Extensions
{
    public static class ApplicationServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Implicit dependency Injection map implementation of Mediator IRequestHandlers and INotificationHandlers
            services.AddScoped<DomainNotificationContext>();
            services.AddMediatR(typeof(ApplicationServiceCollectionExtension));
            services.AddAutoMapper(typeof(ApplicationServiceCollectionExtension));
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceCollectionExtension).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
