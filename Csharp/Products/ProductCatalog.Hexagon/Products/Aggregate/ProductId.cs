using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Hexagon.Products.Aggregate
{
    public class ProductId : Id<Guid>
    {
        public ProductId(Guid internalValue) : base(internalValue)
        {
        }
    }
}