using Doggo.Domain.Interfaces.Repository;
using Doggo.Infra.Data.Repositories.Parametrizacao;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Infra.Data.Extensions
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IBreedRepository, BreedRepository>();
            return services;
        }
    }
}
