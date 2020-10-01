using System.Collections.Generic;
using Pricing.Hexagon.Discounts;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies
{
    public interface IProductGlobalDiscountStrategy
    {
        IDiscount GetDiscount(IReadOnlyCollection<ProductId> productIds);
    }
}