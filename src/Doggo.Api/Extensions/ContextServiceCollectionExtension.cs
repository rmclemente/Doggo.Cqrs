using Doggo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doggo.Api.Extensions
{
    public static class ContextServiceCollectionExtension
    {
        public static IServiceCollection AddContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DoggoCleanContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
