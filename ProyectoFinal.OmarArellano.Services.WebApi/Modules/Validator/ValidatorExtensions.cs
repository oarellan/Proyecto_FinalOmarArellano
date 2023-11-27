using Microsoft.Extensions.DependencyInjection;
using ProyectoFinal.OmarArellano.Application.Validator;

namespace ProyectoFinal.OmarArellano.Services.WebApi.Modules.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<TarjetaCreditoDtoValidator>();
            return services;
        }
    }
}
