using Complejo.Application.Interfaces.Repository;
using Complejo.Application.Interfaces.Repository.Base;
using Complejo.Persistence.Repositories;
using Complejo.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Complejo.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection RegisterPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ---- connection string from appsettigs.json ----
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString")));

            //Transient objects are always different; a new instance is provided to every controller and every service.
            //Scoped objects are the same within a request, but different across different requests.
            //Singleton objects are the same for every object and every request.

            services.AddScoped(typeof(IAsyncRepositoryBase<>), typeof(AsyncRepositoryBase<>));

            services.AddScoped<IFieldRepository, FieldRepository>();
            services.AddScoped<IFieldTypeRepository, FieldTypeRepository>();
            services.AddScoped<IFieldStatusRepository, FieldStatusRepository>();

            return services;
        }
    }
}