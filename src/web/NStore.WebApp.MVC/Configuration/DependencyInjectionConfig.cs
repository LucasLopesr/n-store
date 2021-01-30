using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NStore.WebApp.MVC.Extensions;
using NStore.WebApp.MVC.Services;
using NStore.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;
using System;

namespace NStore.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAutorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();


            var retryWaitPolice = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.WriteLine($"Número de tentativas: {retryCount}");
                });

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAutorizationDelegatingHandler>()
                // .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: _=> TimeSpan.FromMilliseconds(600)));
                .AddPolicyHandler(retryWaitPolice);

            //Exemplo de configuração do refit 
            //services.AddHttpClient("Refit", options => {
            //    options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
            //}).AddHttpMessageHandler<HttpClientAutorizationDelegatingHandler>()
            //.AddTypedClient(Refit.RestService.For<ICatalogoService>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
        }

    }
}
