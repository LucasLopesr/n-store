
using System;
using NStore.Core.DomainObjects;
using NStore.Core.Specifications;
using NStore.Pedidos.Domain.Vouchers.Specifications;

namespace NStore.Pedidos.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public TipoDescontoVoucher TipoDesconto { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        public bool EstaValidoParaUtilizacao()
        {
            return new VoucherAtivoSpecification()
                .And(new VoucherDataSpecification())
                .And(new VoucherQuantidadeSpecification())
                .IsSatisfiedBy(this);
        }

        public void MarcarUtilizado()
        {
            Ativo = false;
            Utilizado = false;
            Quantidade = 0;
        }

    }
}
