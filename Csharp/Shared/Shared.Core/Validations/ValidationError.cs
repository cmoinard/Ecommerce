using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Shared.Core.Validations
{
    public abstract class ValidationError : StructuralEqualityObject
    {
    }

    public static class ValidationErrorExtensions
    {
        public static void EnsureIsValid(this IReadOnlyCollection<ValidationError> errors)
        {
            if (errors.Any())
                throw new ValidationException(errors.ToNonEmptyList());
        }
        
        public static Validation<T> ToValidation<T>(
            this IReadOnlyCollection<ValidationError> errors,
            Func<T> valueWhenValid)
        {
            return
                errors.None()
                    ? Validation.Valid(valueWhenValid())
                    : Validation.Invalid<T>(errors.ToNonEmptyList());
        }
    }
}