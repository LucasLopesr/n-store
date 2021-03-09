using NStore.Carrinho.API.Model.Validations;
using System;
using System.Text.Json.Serialization;

namespace NStore.Carrinho.API.Model
{
    public class CarrinhoItem
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        public Guid CarrinhoId { get; set; }
        [JsonIgnore]
        public CarrinhoCliente CarrinhoCliente { get; set; }
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }

        internal void AssociarCarrinho(Guid carrinhoId)
        {
            CarrinhoId = carrinhoId;
        }

        internal decimal CalcularValor()
        {
            return Quantidade * Valor;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal bool EhValido()
        {
            return new ItemCarrinhoValidation().Validate(this).IsValid;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }
    }
}
