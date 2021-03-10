using System.Threading.Tasks;
using NStore.Bff.Compras.Models;

namespace NStore.Bff.Compras.Services
{
    public interface IPedidoService
    {
        Task<VoucherDto> ObterVoucherPorCodigo(string codigo);
    }
}
