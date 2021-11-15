using Complejo.API.Infrastructure.Security;
using Complejo.API.Middlewares;
using Complejo.Application;
using Complejo.Application.Interfaces.Security;
using Complejo.Identity;
using Complejo.Infrastructure;
using Complejo.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Complejo.API
{
    public class Startup
    {
        private const string KEY_CORS_POLICY_WITH_ORIGINS = "CorsPolicyWithOrigins";
        readonly string CorsPolicyName = "_very_Secure_Cors_Policy_Key";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            AddSwagger(services);

            services.RegisterApplicationServices();
            services.RegisterInfrastructureServices();
            services.RegisterPersistanceServices(Configuration);
            services.RegisterIdentityServices(Configuration);

            services.AddScoped<IUserContext, UserContext>();

            services.AddControllers();

            services.AddCors(opts =>
            {
                opts.AddPolicy(name: CorsPolicyName,
                               builder =>
                               {
                                   builder
                                   .WithOrigins(Environment.GetEnvironmentVariable(KEY_CORS_POLICY_WITH_ORIGINS))
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
                               });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Swagger API Documentation");
            });

            app.UseCustomMiddlewares();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // configuracion JWT swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authentication header using the Bearer scheme. \r\n\r\n
                        Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // configuracion inicial swagger
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Swagger API Documentation",
                });

            });
        }
    }
}
