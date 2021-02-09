using FluentValidation.Results;
using Hs.Core.Messaging;
using MediatR;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hs.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator) => 
            _mediator = mediator;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Task<ValidationResult> SendCommand<T>(T command) where T : Command => 
            _mediator.Send(command);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Task PublishEvent<T>(T @event) where T : Event => 
            _mediator.Publish(@event);
    }
}
