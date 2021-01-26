using Microsoft.Extensions.Options;
using NStore.WebApp.MVC.Extensions;
using NStore.WebApp.MVC.Models;
using NStore.WebApp.MVC.Models.Errors;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient httpClient;

        public AutenticacaoService(HttpClient httpClient,
                                   IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.AutenticacaoUrl);
            this.httpClient = httpClient;
        }

        public async Task<UsuarioAutenticacaoResponse> Login(UsuarioLoginViewModel usuario)
        {
            var usuarioContent = ObterConteudo(usuario);

            var response = await httpClient.PostAsync("/api/identidade/autenticar", usuarioContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioAutenticacaoResponse
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }
            return await DeserializarObjetoResponse<UsuarioAutenticacaoResponse>(response);
        }

        public async Task<UsuarioAutenticacaoResponse> Registrar(UsuarioRegistroViewModel usuario)
        {
            var usuarioContent = ObterConteudo(usuario);

            var response = await httpClient.PostAsync("/api/identidade/nova-conta", usuarioContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioAutenticacaoResponse
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }
            return await DeserializarObjetoResponse<UsuarioAutenticacaoResponse>(response);
        }
    }
}
