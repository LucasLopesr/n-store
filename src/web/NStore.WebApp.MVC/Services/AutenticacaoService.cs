using NStore.WebApp.MVC.Models;
using NStore.WebApp.MVC.Models.Errors;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient httpClient;

        public AutenticacaoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UsuarioAutenticacaoResponse> Login(UsuarioLoginViewModel usuario)
        {
            var usuarioContent = new StringContent(JsonSerializer.Serialize(usuario), encoding: Encoding.UTF8, mediaType: "application/json");

            var response = await httpClient.PostAsync("https://localhost:44399/api/identidade/autenticar", usuarioContent);

            if (!TratarErrosResponse(response)) 
            {
                return new UsuarioAutenticacaoResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                };
            }
            return JsonSerializer.Deserialize<UsuarioAutenticacaoResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<UsuarioAutenticacaoResponse> Registrar(UsuarioRegistroViewModel usuario)
        {
            var usuarioContent = new StringContent(JsonSerializer.Serialize(usuario), encoding: Encoding.UTF8, mediaType: "application/json");

            var response = await httpClient.PostAsync("https://localhost:44399/api/identidade/nova-conta", usuarioContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioAutenticacaoResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                };
            }

            return JsonSerializer.Deserialize<UsuarioAutenticacaoResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
