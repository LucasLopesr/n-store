using NStore.Bff.Compras.Models;
using NStore.Core.Communication;
using System;
using System.Threading.Tasks;

namespace NStore.Bff.Compras.Services
{
    public interface ICarrinhoService
    {
        Task<CarrinhoDto> ObterCarrinho();
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoDTO produto);
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO produto);
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
        Task<ResponseResult> AplicarVoucherCarrinho(VoucherDto voucher);
    }
}
