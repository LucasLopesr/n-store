using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using System;
using System.Net.Http;

namespace NStore.WebApp.MVC.Services
{
    public class PagamentoService : Service, IPagamentoService
    {
        private readonly HttpClient httpClient;

        public PagamentoService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogoUrl);
            this.httpClient = httpClient;
        }
        
    }
}
