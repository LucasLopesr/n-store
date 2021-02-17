using FluentValidation;
using System;

namespace NStore.Carrinho.API.Model.Validations
{
    public class CarrinhoClienteValidation : AbstractValidator<CarrinhoCliente>
    {
        public CarrinhoClienteValidation()
        {
            RuleFor(carrinho => carrinho.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");

            RuleFor(carrinho => carrinho.Itens.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");
                

            RuleFor(carrinho => carrinho.ValorTotal)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}
