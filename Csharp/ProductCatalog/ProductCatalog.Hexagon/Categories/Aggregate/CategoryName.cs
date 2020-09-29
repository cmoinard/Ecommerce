using System.Collections.Generic;
using Shared.Core.DomainModeling;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.Hexagon.Categories
{
    public class CategoryName : StringBasedValueObject
    {
        public const int MaxLength = 100;
        
        public CategoryName(string name) 
            : base(name)
        {
            Validate(name).EnsureIsValid();
        }

        public static Validation<CategoryName> TryCreate(string name) =>
            Validate(name)
                .ToValidation(() => new CategoryName(name));

        private static IReadOnlyCollection<ValidationError> Validate(string? name)
        {
            var errors = new List<ValidationError>();

            var trimmedName = name?.Trim();
            if (trimmedName.IsNullOrWhiteSpace())
            {
                errors.Add(new EmptyValidationError());
            }
            else if (trimmedName.Length > MaxLength)
            {
                errors.Add(new ExceedsMaxLengthValidationError());
            }

            return errors;
        }
        
        public class EmptyValidationError : SimpleValidationError { }
        public class ExceedsMaxLengthValidationError : SimpleValidationError { }
    }
}