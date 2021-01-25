using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NStore.Identidade.API.Models;
using System.Threading.Tasks;

namespace NStore.Identidade.API.Controllers
{
    public class AuthController : Controller
    {

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Registrar(UsuarioRegistroModel usuarioRegistro)
        {
            if (!ModelState.IsValid) return BadRequest();

            var usuario = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(usuario, usuarioRegistro.Senha);

            if (result.Succeeded) 
            {
                await signInManager.SignInAsync(usuario, false);
                return Ok();
            }

            return BadRequest();
        }

        public async Task<ActionResult> Logar(UsuarioLoginModel usuarioLogin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
