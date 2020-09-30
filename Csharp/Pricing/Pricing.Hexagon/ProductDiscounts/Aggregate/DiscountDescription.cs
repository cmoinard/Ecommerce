using Shared.Core.DomainModeling;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate
{
    public class DiscountDescription : StringBasedValueObject
    {
        public DiscountDescription(string description) 
            : base(description)
        {
        }
    }
}