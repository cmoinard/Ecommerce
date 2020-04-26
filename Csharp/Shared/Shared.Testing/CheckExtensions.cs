using NFluent;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace Shared.Testing
{
    public static class CheckExtensions
    {
        public static void ThrowsNotFound<TId>(this ICheck<RunTrace> source, TId id) =>
            source
                .Throws<NotFoundException<TId>>()
                .WithProperty(e => e.Id, id);

        public static void ThrowsValidationException(
            this ICheck<RunTrace> source,
            params ValidationError[] errors)
        {
            source
                .Throws<ValidationException>()
                .WithProperty(
                    e => e.Errors, 
                    errors.ToNonEmptyList());
        }
    }
}