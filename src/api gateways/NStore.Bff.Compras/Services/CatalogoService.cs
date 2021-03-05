using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using NStore.Bff.Compras.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NStore.Bff.Compras.Services
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

        public async Task<ItemProdutoDTO> ObterPorId(Guid id)
        {
            var response = await httpClient.GetAsync($"/catalogo/produtos/{id}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<ItemProdutoDTO>(response);
        }
    }
}
