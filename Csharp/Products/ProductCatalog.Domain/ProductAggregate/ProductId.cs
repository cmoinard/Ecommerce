using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class ProductId : Id<Guid>
    {
        public ProductId(Guid internalValue) : base(internalValue)
        {
        }
    }
}