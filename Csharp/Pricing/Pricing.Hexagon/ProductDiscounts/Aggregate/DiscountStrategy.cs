using Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies;
using Shared.Core.DomainModeling;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate
{
    public class DiscountStrategy : AggregateRoot<DiscountStrategyId>
    {
        public DiscountStrategy(
            DiscountStrategyId id,
            DiscountName name,
            DiscountDescription description,
            IProductGlobalDiscountStrategy strategy) 
            : base(id)
        {
            Name = name;
            Description = description;
            Strategy = strategy;
        }
        
        public DiscountName Name { get; }
        public DiscountDescription Description { get; }
        public IProductGlobalDiscountStrategy Strategy { get; }
    }
}