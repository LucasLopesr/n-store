﻿using System;

namespace NStore.WebApp.MVC.Models
{
    public class ItemCarrinhoViewModel
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
    }
}