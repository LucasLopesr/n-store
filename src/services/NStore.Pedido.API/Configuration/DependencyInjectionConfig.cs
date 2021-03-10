using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NStore.Core.Mediator;
using NStore.Pedidos.API.Application.Queries;
using NStore.Pedidos.Domain.Vouchers;
using NStore.Pedidos.Infra.Data;
using NStore.Pedidos.Infra.Data.Repository;
using NStore.WebApi.Core.Usuario;

namespace NStore.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
           
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();

            services.AddScoped<IVoucherRepository, VoucherRepository>();
            
            services.AddScoped<PedidosContext>();
           

        }
    }
}
