using Microsoft.AspNetCore.Mvc;
using NStore.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController 
    {
        private readonly ICatalogoService catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            this.catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index() 
        {
            var produtos = await catalogoService.ObterTodos();
            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id) 
        {
            return View(await catalogoService.ObterPorId(id));
        }
    }
}
