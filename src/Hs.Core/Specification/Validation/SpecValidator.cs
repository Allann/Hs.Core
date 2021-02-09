using FluentValidation.Results;
using System.Collections.Generic;

namespace Hs.Core.Specificatio
{
    public class SpecValidator<T>
    {
        private readonly Dictionary<string, Rule<T>> _validations = new();

        public ValidationResult Validate(T obj)
        {
            var validationResult = new ValidationResult();
            if (obj is null)
            {
                return new ValidationResult(new[] { new ValidationFailure(string.Empty, "No object passed to validate." )});
            }

            foreach (var rule in _validations.Keys)
            {
                var validation = _validations[rule];
                if (!validation.Validate(obj))
                    validationResult.Errors.Add(new ValidationFailure(obj.GetType().Name, validation.ErrorMessage));
            }

            return validationResult;
        }

        protected void Add(string name, Rule<T> rule)
        {
            _validations.Add(name, rule);
        }

        protected void Remove(string name)
        {
            _validations.Remove(name);
        }

        protected Rule<T>? GetRule(string name)
        {
            _validations.TryGetValue(name, out var rule);
            return rule;
        }
    }
}
