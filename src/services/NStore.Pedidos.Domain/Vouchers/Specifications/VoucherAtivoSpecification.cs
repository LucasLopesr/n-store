using System;
using System.Linq.Expressions;
using NStore.Core.Specifications;

namespace NStore.Pedidos.Domain.Vouchers.Specifications
{
    public class VoucherAtivoSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.Ativo && !voucher.Utilizado;
        }
    }
}
