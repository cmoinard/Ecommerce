using Shared.Core.DomainModeling;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate
{
    public class DiscountName : StringBasedValueObject
    {
        public DiscountName(string name) 
            : base(name)
        {
        }
    }
}