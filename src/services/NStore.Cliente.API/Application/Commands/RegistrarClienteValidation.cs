using FluentValidation;
using NStore.Core.DomainObjects;
using System;

namespace NStore.Cliente.API.Application.Commands
{
    public class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteValidation()
        {
            RuleFor(cliente => cliente.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(cliente => cliente.Nome)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(cliente => cliente.Cpf)
                .Must(TerCpfValido)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(cliente => cliente.Email)
                .Must(TerEmailValido)
                .WithMessage("O e-mail informado não é válido.");
        }

        protected static bool TerCpfValido(string cpf) => Cpf.Validar(cpf);

        protected static bool TerEmailValido(string email) => Email.Validar(email);

    }
}
