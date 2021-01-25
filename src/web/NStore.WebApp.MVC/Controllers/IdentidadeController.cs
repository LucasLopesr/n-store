using Microsoft.AspNetCore.Mvc;
using NStore.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
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
            return RedirectToAction("Index", "Home");
        }
    }
}
