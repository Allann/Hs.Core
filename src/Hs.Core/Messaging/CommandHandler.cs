using FluentValidation.Results;
using Hs.Core.Data;
using System.Threading.Tasks;

namespace Hs.Core.Messaging
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult= new();

        protected CommandHandler()
        { }

        protected void AddError(string mensagem) => 
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));

        protected async Task<ValidationResult> Commit(IUnitOfWork uow, string message)
        {
            if (!await uow.Commit()) AddError(message);

            return ValidationResult;
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow) => 
            await Commit(uow, "There was an error saving data").ConfigureAwait(false);
    }
}
