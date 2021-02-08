using FluentValidation.Results;
using NStore.Core.Messages;
using System;
using System.Threading.Tasks;

namespace NStore.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
