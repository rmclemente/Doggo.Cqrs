using Doggo.Infra.CrossCutting.Communication;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Infra.CrossCutting.Extensions
{
    public static class CrossCuttingServiceCollectionExtension
    {
        public static IServiceCollection AddCrossCuttingServices(this IServiceCollection services)
        {
            services.AddTransient<IMediatorHandler, MediatorHandler>();
            return services;
        }
    }
}
