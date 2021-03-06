﻿using FluentValidation.Results;
using NStore.Carrinho.API.Model.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NStore.Carrinho.API.Model
{
    public class CarrinhoCliente
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
        public ValidationResult ValidationResult { get; set; }

        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }
        public Voucher Voucher { get; set; }

        public CarrinhoCliente(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
        }
        public CarrinhoCliente() { }

        public void AplicarVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorCarrinho();
        }

        private void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;
            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDesconto == TipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        internal bool EhValido()
        {
            var itemValidator = new ItemCarrinhoValidation();
            var erros = Itens.SelectMany(item => itemValidator.Validate(item).Errors).ToList();
            erros.AddRange(new CarrinhoClienteValidation().Validate(this).Errors);

            ValidationResult = new ValidationResult(erros);
            return ValidationResult.IsValid;
        }

        internal void CalcularValorCarrinho()
        {
            ValorTotal = Itens.Sum(item => item.CalcularValor());
            CalcularValorTotalDesconto();
        }

        internal CarrinhoItem ObterPorProdutoId(Guid produtoId)
        {
            return Itens.FirstOrDefault(item => item.ProdutoId == produtoId);
        }

        internal bool CarrinhoItemExistente(CarrinhoItem item)
        {
            return Itens.Any(i => i.ProdutoId == item.ProdutoId);
        }

        internal void AdicionarItem(CarrinhoItem item)
        {
            item.AssociarCarrinho(Id);
            
            if (CarrinhoItemExistente(item))
            {
                var itemExistente = ObterPorProdutoId(item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);

                item = itemExistente;
                Itens.Remove(itemExistente);
            }

            Itens.Add(item);
            
            CalcularValorCarrinho();
        }

        internal void AtualizarItem(CarrinhoItem item)
        {
            item.AssociarCarrinho(Id);
            var itemExistente = ObterPorProdutoId(item.ProdutoId);
            Itens.Remove(itemExistente);
            Itens.Add(item);

            CalcularValorCarrinho();
        }

        internal void AtualizarUnidades(CarrinhoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }
        internal void RemoverItem(CarrinhoItem item)
        {
            Itens.Remove(ObterPorProdutoId(item.ProdutoId));

            CalcularValorCarrinho();
        }

    }
}
