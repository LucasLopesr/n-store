using FluentValidation;
using System;


namespace NStore.Carrinho.API.Model.Validations
{
    public class ItemPedidoValidation : AbstractValidator<CarrinhoItem>
    {
        public ItemPedidoValidation()
        {
            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade mínima de um item é 1");

            RuleFor(c => c.Valor)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");

        }
    }
}
