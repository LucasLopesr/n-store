using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NStore.Identidade.API.Extensions;
using NStore.Identidade.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static NStore.Identidade.API.Utils.MessagesUtils;

namespace NStore.Identidade.API.Controllers
{
    [Route("api/identidade")]
    public class AuthController : MainController
    {

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AppSettings appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, 
                              UserManager<IdentityUser> userManager, 
                              IOptions<AppSettings> appSettings)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistroModel usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuario = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(usuario, usuarioRegistro.Senha);

            if (result.Succeeded) 
            {
                return CustomResponse(await GerarJwt(usuarioRegistro.Email));
            }

            foreach (var erro in result.Errors)
                AddErroProcessamento(erro.Description);
            
            return CustomResponse();
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar(UsuarioLoginModel usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut) 
            {
                AddErroProcessamento(USUARIO_BLOQUEADO_POR_TENTATIVAS);
                return CustomResponse();
            }

            AddErroProcessamento(USUARIO_SENHA_INVALIDOS);

            return CustomResponse();
        }

        private async Task<UsuarioAutenticacaoResponse> GerarJwt(string email) 
        {
            var usuario = await userManager.FindByEmailAsync(email);
            var claims = await userManager.GetClaimsAsync(usuario);
            var usuarioRoles = await userManager.GetRolesAsync(usuario);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToEpochDate().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToEpochDate().ToString(), ClaimValueTypes.Integer64));

            foreach (var role in usuarioRoles) 
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor 
            { 
                Issuer = appSettings.Emissor,
                Audience = appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new UsuarioAutenticacaoResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(appSettings.ExpiracaoHoras).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Claims = claims.Select(claim => new UsuarioClaim { Type = claim.Type, Value = claim.Value })
                }
            };
        }

    }
}
