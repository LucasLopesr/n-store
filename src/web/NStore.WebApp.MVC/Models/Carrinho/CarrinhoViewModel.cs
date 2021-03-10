using System.Collections.Generic;
using System.Security.AccessControl;

namespace NStore.WebApp.MVC.Models.Carrinho
{
    public class CarrinhoViewModel
    {
        public decimal ValorTotal { get; set; }
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new List<ItemCarrinhoViewModel>();

        public VoucherViewModel Voucher { get; set; }

        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }
    }
}