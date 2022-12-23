using psychic_pancake.servicos;
using Microsoft.Extensions.DependencyInjection;
using psychic_pancake.models;

namespace psychic_pancake
{
    public static class Services
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ClienteService>();
            services.AddSingleton<FilmeService>();
            services.AddSingleton<LocacaoService>();

            return services;
        }
    }
}