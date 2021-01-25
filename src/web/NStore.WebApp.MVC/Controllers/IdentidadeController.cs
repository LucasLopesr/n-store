using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NStore.WebApp.MVC.Models;
using NStore.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            await RealizarLogin(resposta);
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
            await RealizarLogin(resposta);
            return RedirectToAction("Index", "Home");
        }

        private async Task RealizarLogin(UsuarioAutenticacaoResponse resposta) 
        {
            var token = ObterTokenFormatado(resposta.AccessToken);
            var claims = new List<Claim>();

            claims.Add(new Claim("JWT", resposta.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken) 
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        }
    }
}
