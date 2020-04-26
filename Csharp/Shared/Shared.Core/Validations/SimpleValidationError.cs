using System.Collections.Generic;

namespace Shared.Core.Validations
{
    public abstract class SimpleValidationError : ValidationError
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield break;
        }
    }
}