using System.Collections.Generic;
using System.Text.RegularExpressions;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace ProductCatalog.Hexagon.Products.Aggregate
{
    public class ProductName : StringBasedValueObject
    {
        public const int MaxLength = 100;
        
        private static Regex AllowedCharsRegex { get; } = 
            new Regex(@"^[\p{L}\d\.\/\ -,]+$", RegexOptions.IgnoreCase);

        public ProductName(string name)
            : base(name.Trim())
        {
            Validate(name).EnsureIsValid();
        }

        public static Validation<ProductName> TryCreate(string name) => 
            Validate(name)
                .ToValidation(() => new ProductName(name));

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

                if (!AllowedCharsRegex.IsMatch(trimmedName))
                    errors.Add(new InvalidCharactersValidationError());
            }

            return errors;
        }
        
        public class EmptyValidationError : SimpleValidationError { }
        public class ExceedsMaxLengthValidationError : SimpleValidationError { }
        public class InvalidCharactersValidationError : SimpleValidationError { }
    }
}