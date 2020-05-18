using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Shared.Core.DomainModeling;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.Hexagon.Categories.Aggregate
{
    public class CategoryName : StringBasedValueObject
    {
        public const int MaxLength = 50;
        
        private static Regex AllowedCharsRegex { get; } = 
            new Regex(@"^[\p{L}\d\.\/\ -,]+$", RegexOptions.IgnoreCase);

        public CategoryName(string name)
            : base(name.Trim())
        {
            var trimmedValue = name.Trim();
            
            var errors = Validate(trimmedValue);
            if (errors.Any())
                throw new ValidationException(errors.ToNonEmptyList());
        }

        public static Validation<CategoryName> TryCreate(string name)
        {
            var errors = Validate(name);
            return
                errors.None()
                    ? Validation.Valid(new CategoryName(name))
                    : Validation.Invalid<CategoryName>(errors.ToNonEmptyList());
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