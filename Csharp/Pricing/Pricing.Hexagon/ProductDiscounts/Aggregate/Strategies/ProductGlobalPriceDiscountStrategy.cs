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
        
        public IDiscount GetDiscount(IReadOnlyCollection<ProductPrice> productPrices)
        {
            var totalPrice = _priceByProductId.Values.Sum();
            return totalPrice > PriceThreshold
                ? Discount.Price(25)
                : Discount.None;
        }
    }
}