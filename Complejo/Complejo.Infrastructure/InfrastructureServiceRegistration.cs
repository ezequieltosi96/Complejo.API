using Complejo.Application.Interfaces.Mapping;
using Complejo.Infrastructure.Automapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Complejo.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IMapping, Mapping>();

            return services;
        }
    }
}
