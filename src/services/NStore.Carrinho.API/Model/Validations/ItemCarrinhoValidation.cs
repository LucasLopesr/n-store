using FluentValidation;
using System;


namespace NStore.Carrinho.API.Model.Validations
{
    public class ItemCarrinhoValidation : AbstractValidator<CarrinhoItem>
    {
        public ItemCarrinhoValidation()
        {
            RuleFor(item => item.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(item => item.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(item => item.Quantidade)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade mínima de {item.Nome} é 1");

            RuleFor(item => item.Valor)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Nome} precisa ser maior que 0");

        }
    }
}
