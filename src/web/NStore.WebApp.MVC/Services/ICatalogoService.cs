using NStore.WebApp.MVC.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStore.WebApp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterTodos();
        Task<ProdutoViewModel> ObterPorId(Guid id);
    }
}
