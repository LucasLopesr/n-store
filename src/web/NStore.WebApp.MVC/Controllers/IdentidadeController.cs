using Microsoft.AspNetCore.Mvc;
using NStore.WebApp.MVC.Models;
using NStore.WebApp.MVC.Services;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {

        private readonly IAutenticacaoService autenticacaoService;

        public IdentidadeController(IAutenticacaoService autenticacaoService)
        {
            this.autenticacaoService = autenticacaoService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registrar(UsuarioRegistroViewModel usuario)
        {
            if (!ModelState.IsValid) return View(usuario);

            var resposta = await autenticacaoService.Registrar(usuario);

            return RedirectToAction("Login", "Identidade");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login() 
        {
            return View();
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            return View();
        }
    
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UsuarioLoginViewModel usuario) 
        {
            if (!ModelState.IsValid) return View(usuario);

            var resposta = await autenticacaoService.Login(usuario);

            return RedirectToAction("Index", "Home");
        }
    }
}
