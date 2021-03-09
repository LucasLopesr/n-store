using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NStore.Pedidos.API.Application.DTO;
using NStore.Pedidos.Domain.Vouchers;

namespace NStore.Pedidos.API.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            this.voucherRepository = voucherRepository;
        }

        public async Task<VoucherDto> ObterVoucherPorCodigo(string codigo)
        {
            var voucher =  await voucherRepository.ObterVoucherPorCodigo(codigo);

            if (voucher == null) return null;

            if (voucher.EstaValidoParaUtilizacao()) return null;

            return new VoucherDto
            {
                Codigo = voucher.Codigo,
                Percentual = voucher.Percentual,
                TipoDesconto = (int) voucher.TipoDesconto,
                ValorDesconto = voucher.ValorDesconto
            };
        }
    }
}
