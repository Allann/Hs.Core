using FluentValidation.Results;

namespace Hs.Core.Messaging
{
    public class ResponseMessage : Message
    {
        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; }

        public void AddError(string message)
        {
            AddError(string.Empty, message);
        }

        public void AddError(string propertyName, string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(propertyName, message));
        }
    }
}
