using System.Collections.Generic;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace Pricing.Hexagon.Discounts
{
    public class DiscountPercent : SimpleValueObject<int>, IDiscount
    {
        public DiscountPercent(int value) 
            : base(value)
        {
            Validate(value).EnsureIsValid();
        }

        public Price ApplyOn(Price price) => 
            price * (InternalValue / 100m);

        private static IReadOnlyCollection<ValidationError> Validate(int value)
        {
            var errors = new List<ValidationError>();
            if (value < 0 || 100 < value)
            {
                errors.Add(new Outside0And100ValidationError());
            }

            return errors;
        }
        public class Outside0And100ValidationError : SimpleValidationError{}
    }
}