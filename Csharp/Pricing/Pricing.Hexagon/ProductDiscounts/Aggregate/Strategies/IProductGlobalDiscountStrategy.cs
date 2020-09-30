using System.Collections.Generic;
using Pricing.Hexagon.Discounts;
using Pricing.Hexagon.Products.Aggregate;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies
{
    public interface IProductGlobalDiscountStrategy
    {
        IDiscount GetDiscount(IReadOnlyCollection<ProductPrice> productPrices);
    }
}