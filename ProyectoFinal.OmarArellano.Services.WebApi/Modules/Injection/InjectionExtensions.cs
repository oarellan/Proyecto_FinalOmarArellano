using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoFinal.OmarArellano.Application.Interface;
using ProyectoFinal.OmarArellano.Application.Main;
using ProyectoFinal.OmarArellano.Domain.Core;
using ProyectoFinal.OmarArellano.Domain.Interface;
using ProyectoFinal.OmarArellano.Infrastructure.Data;
using ProyectoFinal.OmarArellano.Infrastructure.Interface;
using ProyectoFinal.OmarArellano.Infrastructure.Repository;
using ProyectoFinal.OmarArellano.Transversal.Common;
using ProyectoFinal.OmarArellano.Transversal.Logging;

namespace ProyectoFinal.OmarArellano.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<DapperContext>();
            services.AddScoped<ITarjetaCreditoApplication, TarjetaCreditoApplication>();
            services.AddScoped<ITarjetaCreditoDomain, TarjetaCreditoDomain>();
            services.AddScoped<ITarjetaCreditoRepository, TarjetaCreditoRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
