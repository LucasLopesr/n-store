using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NStore.Core.Data;
using NStore.Pedidos.Domain.Vouchers;

namespace NStore.Pedidos.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {

        private readonly PedidosContext context;

        public VoucherRepository(PedidosContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => context;
        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await context.Vouchers.FirstOrDefaultAsync(voucher => voucher.Codigo == codigo);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
