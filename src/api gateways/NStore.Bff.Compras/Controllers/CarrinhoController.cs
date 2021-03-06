﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IPedidoService pedidoService;

        public CarrinhoController(
            ICarrinhoService carrinhoService, 
            ICatalogoService catalogoService, 
            IPedidoService pedidoService)
        {
            this.catalogoService = catalogoService;
            this.pedidoService = pedidoService;
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
            return await carrinhoService.ObterQuantidadeCarrinho();
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

        [HttpPost]
        [Route("compras/carrinho/aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
        {
            var voucher = await pedidoService.ObterVoucherPorCodigo(voucherCodigo);
            if (voucher is null)
            {
                AddErroProcessamento("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }

            var resposta = await carrinhoService.AplicarVoucherCarrinho(voucher);
            return CustomResponse(resposta);
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
