using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NStore.WebApp.MVC.Models;
using NStore.WebApp.MVC.Models.Catalogo;
using NStore.WebApp.MVC.Services;

namespace NStore.WebApp.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly IComprasBffService comprasBffService;

        public CarrinhoController(IComprasBffService carrinhoService)
        {
            comprasBffService = carrinhoService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await comprasBffService.ObterCarrinho());
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoViewModel itemProduto)
        {
            var resposta = await comprasBffService.AdicionarItemCarrinho(itemProduto);

            if (ResponsePossuiErros(resposta)) return View("Index", await comprasBffService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var itemProduto = new ItemCarrinhoViewModel { ProdutoId = produtoId, Quantidade = quantidade };
            var resposta = await comprasBffService.AtualizarItemCarrinho(produtoId, itemProduto);

            if (ResponsePossuiErros(resposta)) return View("Index", await comprasBffService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var resposta = await comprasBffService.RemoverItemCarrinho(produtoId);

            if (ResponsePossuiErros(resposta)) return View("Index", await comprasBffService.ObterCarrinho());

            return RedirectToAction("Index");
        }
    }
}