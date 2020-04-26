using System;
using Shared.Core.Validations;

namespace Shared.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(ValidationError error)
            : this(new NonEmptyList<ValidationError>(error))
        {
        }
        
        public ValidationException(NonEmptyList<ValidationError> errors)
        {
            Errors = errors;
        }

        public NonEmptyList<ValidationError> Errors { get; }
    }
}