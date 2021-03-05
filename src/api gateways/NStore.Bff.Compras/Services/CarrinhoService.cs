using Microsoft.Extensions.Options;
using NStore.Bff.Compras.Extensions;
using NStore.Bff.Compras.Models;
using NStore.Core.Communication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NStore.Bff.Compras.Services
{
    public class CarrinhoService : Service, ICarrinhoService
    {
        private readonly HttpClient httpClient;

        public CarrinhoService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogoUrl);
            this.httpClient = httpClient;
        }

        public async Task<CarrinhoDTO> ObterCarrinho()
        {
            var response = await httpClient.GetAsync("/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoDTO>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoDTO produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await httpClient.PostAsync("/carrinho/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await httpClient.PutAsync($"/carrinho/{produto.ProdutoId}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await httpClient.DeleteAsync($"/carrinho/{produtoId}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
