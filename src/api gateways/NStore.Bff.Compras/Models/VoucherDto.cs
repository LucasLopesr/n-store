﻿namespace NStore.Bff.Compras.Models
{
    public class VoucherDto
    {
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int TipoDesconto { get; set; }
    }
}
