using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.Specifications.Validation
{
    public class Rule<T>
    {
        private readonly Specification<T> specificationSpec;

        public Rule(Specification<T> spec, string errorMessage)
        {
            specificationSpec = spec;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool Validate(T obj)
        {
            return specificationSpec.IsSatisfiedBy(obj);
        }
    }
}
