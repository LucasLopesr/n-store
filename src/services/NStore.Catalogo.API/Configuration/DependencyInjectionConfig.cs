using Microsoft.Extensions.DependencyInjection;
using NStore.Catalogo.API.Data;
using NStore.Catalogo.API.Data.Repository;
using NStore.Catalogo.API.Models;

namespace NStore.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
