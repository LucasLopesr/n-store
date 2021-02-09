using FluentValidation.Results;
using MediatR;
using NStore.Cliente.API.Application.Events;
using NStore.Cliente.API.Models;
using NStore.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NStore.Cliente.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {

        private readonly IClienteRepository clienteRepository;
        public ClienteCommandHandler(IClienteRepository clienteRepository) 
        {
            this.clienteRepository = clienteRepository;
        }
        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (message.EhValido()) return message.ValidationResult;

            var cliente = new Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            var clienteExistente = clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clienteExistente != null) 
            {
                AdicionarErro("Este CPF já está em uso.");
                return message.ValidationResult;
            }
            clienteRepository.Adicionar(cliente);
            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(clienteRepository.UnitOfWork);
        }
    }
}
