using FluentValidation.Results;
using Hs.Core.Messaging;
using System.Threading.Tasks;

namespace Hs.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;

        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
