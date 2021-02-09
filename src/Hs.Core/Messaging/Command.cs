using FluentValidation.Results;
using MediatR;
using System;

namespace Hs.Core.Messaging
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new ValidationResult();
        }

        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() => 
            ValidationResult.IsValid;
    }
}
