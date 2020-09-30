using System.Collections.Generic;
using Pricing.Hexagon.Discounts;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies
{
    public class ProductGlobalWeightDiscountStrategy : IProductGlobalDiscountStrategy
    {
        private static readonly Weight WeightThreshold = Weight.Kg(50);
        
        private readonly IReadOnlyDictionary<ProductId, Weight> _weightByProductId;

        public ProductGlobalWeightDiscountStrategy(
            IReadOnlyDictionary<ProductId, Weight> weightByProductId)
        {
            _weightByProductId = weightByProductId;
        }

        public IDiscount GetDiscount(IReadOnlyCollection<ProductPrice> productPrices)
        {
            var totalWeight = productPrices.Sum(pp => _weightByProductId[pp.Id]);
            return totalWeight >= WeightThreshold
                ? Discount.Percent(5)
                : Discount.None;
        }
    }
}