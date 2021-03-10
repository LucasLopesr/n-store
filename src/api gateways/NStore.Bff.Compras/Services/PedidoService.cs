using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using NStore.Bff.Compras.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NStore.Bff.Compras.Services
{
    public class PedidoService : Service, IPedidoService
    {
        private readonly HttpClient httpClient;

        public PedidoService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.PedidoUrl);
            this.httpClient = httpClient;
        }

        public async Task<VoucherDto> ObterVoucherPorCodigo(string codigo)
        {
            var response = await httpClient.GetAsync($"/vouchers/{codigo}/");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VoucherDto>(response);
        }
    }
}
