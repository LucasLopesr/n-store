using NStore.Bff.Compras.Models;
using System;
using System.Threading.Tasks;

namespace NStore.Bff.Compras.Services
{
    public interface ICatalogoService
    {
        Task<ItemProdutoDTO> ObterPorId(Guid id);
    }
}
