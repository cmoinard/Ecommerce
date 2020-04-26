using System.Collections.Generic;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.ApplicationServices.Categories
{
    public class UnvalidatedCategoryState : IValidatable
    {
        public UnvalidatedCategoryState(string name)
        {
            Name = name;
        }

        public string Name { get; }
        
        public IEnumerable<ValidationError> Validate()
        {
            if (Name.IsNullOrWhiteSpace())
                yield return new EmptyCategoryNameValidationError();
        }
    }
    
    public class EmptyCategoryNameValidationError : SimpleValidationError
    {
    }
}