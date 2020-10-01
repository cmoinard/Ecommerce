using System.Collections.Generic;
using Pricing.Hexagon.Discounts;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies
{
    public class ProductGlobalPriceDiscountStrategy : IProductGlobalDiscountStrategy
    {
        private static readonly Price PriceThreshold = new Price(500);
        
        private readonly IReadOnlyDictionary<ProductId, Price> _priceByProductId;

        public ProductGlobalPriceDiscountStrategy(
            IReadOnlyDictionary<ProductId, Price> priceByProductId)
        {
            _priceByProductId = priceByProductId;
        }
        
        public IDiscount GetDiscount(IReadOnlyCollection<ProductId> productIds)
        {
            var totalPrice = productIds.Sum(id => _priceByProductId[id]);
            return totalPrice >= PriceThreshold
                ? Discount.Price(25)
                : Discount.None;
        }
    }
}