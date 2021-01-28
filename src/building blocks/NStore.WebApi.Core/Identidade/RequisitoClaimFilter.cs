using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace NStore.WebApi.Core.Identidade
{
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim claim;

        public RequisitoClaimFilter(Claim claim)
        {
            this.claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated) 
            {
                context.Result = new StatusCodeResult(401);
            }

            if (!CustomAuthorize.ValidarClaimsUsuario(context.HttpContext, claim.Type, claim.Value)) 
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}