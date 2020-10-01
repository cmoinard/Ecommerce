using Shared.Core.DomainModeling;
using Shared.Domain;

namespace Pricing.Hexagon.Products.Aggregate
{
    public class ProductPrice : AggregateRoot<ProductId>
    {
        public ProductPrice(ProductId id, Price price) 
            : base(id)
        {
            Price = price;
        }

        public Price Price { get; set; }
    }
}