using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NStore.WebApp.MVC.Extensions;
using NStore.WebApp.MVC.Services;
using NStore.WebApp.MVC.Services.Handlers;

namespace NStore.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient<HttpClientAutorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<ICatalogoService, CatalogoService>().AddHttpMessageHandler<HttpClientAutorizationDelegatingHandler>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
