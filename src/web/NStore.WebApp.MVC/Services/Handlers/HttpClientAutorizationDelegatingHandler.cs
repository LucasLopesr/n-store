

using NStore.WebApp.MVC.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Services.Handlers
{
    public class HttpClientAutorizationDelegatingHandler : DelegatingHandler
    {

        private readonly IUser user;

        public HttpClientAutorizationDelegatingHandler(IUser user)
        {
            this.user = user;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) 
        {
            var authorizationHeader = user.ObterHttpContext().Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string> { authorizationHeader });
            }

            var token = user.ObterUserToken();

            if (!string.IsNullOrWhiteSpace(token)) 
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
