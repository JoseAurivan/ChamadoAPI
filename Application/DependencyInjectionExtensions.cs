using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IChamadoService, ChamadoService>()
                .AddScoped<IAtendenteService, AtendenteService>()
                .AddScoped<IClienteService, ClienteService>();
            return serviceCollection;
        }
    }
}