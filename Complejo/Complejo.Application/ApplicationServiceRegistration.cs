using Complejo.Application.Validators.Field;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Complejo.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblyContaining<CreateFieldValidator>();

            return services;
        }
    }
}
