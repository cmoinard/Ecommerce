using Shared.Core.DomainModeling;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate
{
    public class DiscountStrategyId : Id<int>
    {
        public DiscountStrategyId(int internalValue) : base(internalValue)
        {
        }
    }
}