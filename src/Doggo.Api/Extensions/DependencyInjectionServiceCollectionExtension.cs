using Doggo.Application.Extensions;
using Doggo.Infra.CrossCutting.Extensions;
using Doggo.Infra.Data.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Api.Extensions
{
    public static class DependencyInjectionServiceCollectionExtension
    {
        public static IServiceCollection AddRegisteredServices(this IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCrossCuttingServices();
            services.AddRepositoryServices();
            services.AddApplicationServices();

            return services;
        }
    }
}
