using System.Collections.Generic;
using Pricing.Hexagon.Discounts;
using Shared.Core.DomainModeling;
using Shared.Domain;

namespace Pricing.Hexagon.Sales
{
    public class ProductDiscount : ValueObject
    {
        public ProductDiscount(ProductId productId, IDiscount discount)
        {
            Discount = discount;
            ProductId = productId;
        }

        public IDiscount Discount { get; }
        public ProductId ProductId { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Discount;
            yield return ProductId;
        }
    }
}