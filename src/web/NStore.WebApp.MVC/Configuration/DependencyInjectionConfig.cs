using Microsoft.Extensions.DependencyInjection;
using NStore.WebApp.MVC.Services;

namespace NStore.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
        }
    }
}
