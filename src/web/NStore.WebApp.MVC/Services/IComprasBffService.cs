using System;
using System.Threading.Tasks;
using NStore.Core.Communication;
using NStore.WebApp.MVC.Models;

namespace NStore.WebApp.MVC.Services
{
    public interface IComprasBffService
    {
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto);
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel produto);
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
    }
}