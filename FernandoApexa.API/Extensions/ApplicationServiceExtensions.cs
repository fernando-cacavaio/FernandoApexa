using FernandoApexa.Application.Advisors;
using FernandoApexa.Application.Advisors.Commands;
using FernandoApexa.Application.Cache;
using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using FernandoApexa.Persistence;
using FernandoApexa.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace FernandoApexa.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<FernandoApexaDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(config.GetConnectionString("FernandoDB"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();
            services.AddHttpContextAccessor();
            services.AddScoped<IHealthStatus, HealthStatus>();
            services.AddScoped<IAdvisorRepository, AdvisorRepository>();
            services.AddSingleton<ICache<string, object>, MRUCache<string, object>>();

            return services;
        }
    }
}