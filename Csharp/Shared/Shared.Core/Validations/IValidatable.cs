using System.Collections.Generic;
using System.Linq;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Shared.Core.Validations
{
    public interface IValidatable
    {
        IEnumerable<ValidationError> Validate();
    }

    public static class ValidatableExtensions
    {
        public static void EnsureIsValid(this IValidatable validatable)
        {
            var errors = validatable.Validate().ToList();
            if (errors.Any())
                throw new ValidationException(errors.ToNonEmptyList());
        }
    }
}