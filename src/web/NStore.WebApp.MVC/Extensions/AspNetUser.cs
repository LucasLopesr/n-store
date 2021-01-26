using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NStore.WebApp.MVC.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor accessor;

        public AspNetUser(IHttpContextAccessor httpContextAccessor)
        {
            accessor = httpContextAccessor;
        }

        public string Name => accessor.HttpContext.User.Identity.Name;

        public bool EstaAutenticado()
        {
            return accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return accessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return accessor.HttpContext;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? accessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? accessor.HttpContext.User.GetUserToken() : string.Empty;
        }

        public bool PossuiRole(string role)
        {
            return accessor.HttpContext.User.IsInRole(role);
        }
    }
    
}
