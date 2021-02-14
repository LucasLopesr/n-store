
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NStore.Cliente.API.Application.Commands;
using NStore.Core.Mediator;
using NStore.Core.Messages.Integration;
using NStore.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NStore.Cliente.API.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus bus;
        private readonly IServiceProvider serviceProvider;

        public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            this.serviceProvider = serviceProvider;
            this.bus = bus;
        }

        private void SetResponder()
        {
            bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request => await RegistrarCliente(request));
            bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            return Task.CompletedTask;
        }

        private void OnConnect(object sender, EventArgs eventArgs)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso;
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
