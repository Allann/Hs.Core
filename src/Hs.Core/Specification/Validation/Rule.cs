using Hs.Core.Specification;

namespace Hs.Core.Specificatio
{
    public class Rule<T>
    {
        private readonly Specification<T> _specificationSpec;

        public Rule(Specification<T> spec, string errorMessage)
        {
            _specificationSpec = spec;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool Validate(T obj) => 
            _specificationSpec.IsSatisfiedBy(obj);
    }
}
