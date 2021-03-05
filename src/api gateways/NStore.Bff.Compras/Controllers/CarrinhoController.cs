using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NStore.Bff.Compras.Models;
using NStore.Bff.Compras.Services;
using NStore.WebApi.Core.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Bff.Compras.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService carrinhoService;
        private readonly ICatalogoService catalogoService;

        public CarrinhoController(
            ICarrinhoService carrinhoService, 
            ICatalogoService catalogoService)
        {
            this.catalogoService = catalogoService;
            this.carrinhoService = carrinhoService;
        }

        [HttpGet]
        [Route("compras/carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await carrinhoService.ObterCarrinho());
        }

        [HttpGet]
        [Route("compras/carrinho-quantidade")]
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var carrinho = await carrinhoService.ObterCarrinho();
            return carrinho?.Itens.Sum(i => i.Quantidade) ?? 0;
        }

        [HttpPost]
        [Route("compras/carrinho/items")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoDTO item)
        {
            var produto = await catalogoService.ObterPorId(item.ProdutoId);

            await ValidarItemCarrinho(produto, item.Quantidade);
            if (!IsOperacaoValida()) return CustomResponse();

            item.Nome = produto.Nome;
            item.Valor = produto.Valor;
            item.Imagem = produto.Imagem;

            return CustomResponse(await carrinhoService.AdicionarItemCarrinho(item));
        }

        [HttpPut]
        [Route("compras/carrinho/items/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO item)
        {

            var produto = await catalogoService.ObterPorId(item.ProdutoId);

            await ValidarItemCarrinho(produto, item.Quantidade);
            if (!IsOperacaoValida()) return CustomResponse();

            return CustomResponse(await carrinhoService.AtualizarItemCarrinho(produtoId, item));
        }

        [HttpDelete]
        [Route("compras/carrinho/items/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await catalogoService.ObterPorId(produtoId);

            if (produto == null)
            {
                AddErroProcessamento("Produto inexistente!");
                return CustomResponse();
            }

            return CustomResponse(await carrinhoService.RemoverItemCarrinho(produtoId));
        }

        private async Task ValidarItemCarrinho(ItemProdutoDTO produto, int quantidadeSelecionada)
        {
            if (quantidadeSelecionada == null) AddErroProcessamento("Produto inexistente!");
            if (quantidadeSelecionada < 1) AddErroProcessamento($"Escolha ao menos uma unidade do produto {produto.Nome}");

            var carrinho = await carrinhoService.ObterCarrinho();
            var itemCarrinho = carrinho.Itens.FirstOrDefault(item => item.ProdutoId == produto.Id);

            if (itemCarrinho != null && itemCarrinho.Quantidade + quantidadeSelecionada > produto.QuantidadeEstoque)
            {
                AddErroProcessamento(FormatarMensagemQuantidadeMaximaEstoque(produto.Nome, produto.QuantidadeEstoque, quantidadeSelecionada));
                return;
            }

            if (quantidadeSelecionada > produto.QuantidadeEstoque) 
                AddErroProcessamento(FormatarMensagemQuantidadeMaximaEstoque(produto.Nome, produto.QuantidadeEstoque, quantidadeSelecionada));
        }

        private string FormatarMensagemQuantidadeMaximaEstoque(string nomeProduto, int quantidadeEmEstoque, int quantidadeSelecionada)
            => $"O produto {nomeProduto} possui {quantidadeEmEstoque} unidades em estoque, você selecionou {quantidadeSelecionada}";
    }
}
