
namespace NStore.Carrinho.API.Model
{
    public class Voucher
    {
        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public TipoDescontoVoucher TipoDesconto { get; private set; }
    }
}
