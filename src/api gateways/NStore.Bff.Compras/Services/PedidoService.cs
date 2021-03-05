using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using System;
using System.Net.Http;

namespace NStore.Bff.Compras.Services
{
    public class PedidoService : Service, IPedidoService
    {
        private readonly HttpClient httpClient;

        public PedidoService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogoUrl);
            this.httpClient = httpClient;
        }
        
    }
}
