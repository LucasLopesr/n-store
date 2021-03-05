using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NStore.Bff.Compras.Services;
using NStore.WebApi.Core.Controllers;
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
            return CustomResponse();
        }

        [HttpGet]
        [Route("compras/carrinho-quantidade")]
        public async Task<IActionResult> ObterQuantidadeCarrinho()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("compras/carrinho/items")]
        public async Task<IActionResult> AdicionarItemCarrinho()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("compras/carrinho/items/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("compras/carrinho/items/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho()
        {
            return CustomResponse();
        }
    }
}
