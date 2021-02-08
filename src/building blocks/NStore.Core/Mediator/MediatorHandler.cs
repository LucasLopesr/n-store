using FluentValidation.Results;
using MediatR;
using NStore.Core.Messages;
using System.Threading.Tasks;

namespace NStore.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command
        {
            return await mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await mediator.Publish(evento);
        }
    }
}
