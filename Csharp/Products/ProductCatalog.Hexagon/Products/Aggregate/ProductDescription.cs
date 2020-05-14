using System.Collections.Generic;
using System.Linq;
using Shared.Core.DomainModeling;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.Hexagon.Products.Aggregate
{
    public class ProductDescription : StringBasedValueObject
    {
        public const int MaxLength = 2000;

        public ProductDescription(string name)
            : base(name.Trim())
        {
            var trimmedValue = name.Trim();
            
            var errors = Validate(trimmedValue);
            if (errors.Any())
                throw new ValidationException(errors.ToNonEmptyList());
        }

        public static Validation<ProductDescription> TryCreate(string name)
        {
            var errors = Validate(name);
            return
                errors.None()
                    ? Validation.Valid(new ProductDescription(name))
                    : Validation.Invalid<ProductDescription>(errors.ToNonEmptyList());
        }
        
        private static IReadOnlyCollection<ValidationError> Validate(string name)
        {
            var errors = new List<ValidationError>();
            
            var trimmedName = name.Trim();
            
            if (string.IsNullOrWhiteSpace(trimmedName))
                errors.Add(new EmptyValidationError());
            else
            {
                if (trimmedName.Length > MaxLength)
                    errors.Add(new ExceedsMaxLengthValidationError());
            }

            return errors;
        }
        
        public class EmptyValidationError : SimpleValidationError { }
        public class ExceedsMaxLengthValidationError : SimpleValidationError { }
    }
}