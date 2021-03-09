using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NStore.Pedidos.API.Application.DTO;

namespace NStore.Pedidos.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDto> ObterVoucherPorCodigo(string codigo);
    }
}
