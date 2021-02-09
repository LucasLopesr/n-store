using FluentValidation.Results;
using MediatR;
using System;

namespace NStore.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {

            return true;
        }
    }
}
