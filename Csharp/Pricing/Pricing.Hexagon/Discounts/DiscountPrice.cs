using System.Collections.Generic;
using Pricing.Hexagon.Products;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace Pricing.Hexagon.Discounts
{
    public class DiscountPrice : SimpleValueObject<decimal>, IDiscount
    {
        public DiscountPrice(decimal value) 
            : base(value)
        {
            Validate(value).EnsureIsValid();
        }

        public Price ApplyOn(Price price) => 
            price - InternalValue;

        private static IReadOnlyCollection<ValidationError> Validate(decimal price)
        {
            var errors = new List<ValidationError>();
            if (price < 0)
                errors.Add(new NegativePriceValidationError());
            
            if (price % 0.01m != 0)
                errors.Add(new BelowCentValidationError());

            return errors;
        }

        public class NegativePriceValidationError : SimpleValidationError{}

        public class BelowCentValidationError : SimpleValidationError{}
    }
}