using Microsoft.Extensions.Options;
using NStore.WebApp.MVC.Extensions;
using NStore.WebApp.MVC.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Services
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient httpClient;

        public CatalogoService(HttpClient httpClient,
                                   IOptions<AppSettings> appSettings)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.CatalogoUrl);
            this.httpClient = httpClient;
        }
        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = await httpClient.GetAsync($"/catalogo/produtos/{id}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = await httpClient.GetAsync("/catalogo/produtos");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }
    }
}
