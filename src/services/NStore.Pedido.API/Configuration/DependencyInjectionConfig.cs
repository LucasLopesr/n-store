using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NStore.Core.Mediator;
using NStore.Pedidos.Infra.Data;
using NStore.WebApi.Core.Usuario;

namespace NStore.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
           // services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<PedidosContext>();
           

        }
    }
}
