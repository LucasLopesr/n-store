using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using System;
using System.Net.Http;

namespace NStore.Bff.Compras.Services
{
    public class PagamentoService : Service, IPagamentoService
    {
        private readonly HttpClient httpClient;

        public PagamentoService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.PagamentoUrl);
            this.httpClient = httpClient;
        }
        
    }
}
