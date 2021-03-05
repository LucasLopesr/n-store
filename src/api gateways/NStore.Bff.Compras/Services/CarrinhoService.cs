using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using System;
using System.Net.Http;

namespace NStore.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient httpClient;

        public CatalogoService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogoUrl);
            this.httpClient = httpClient;
        }
        
    }
}
