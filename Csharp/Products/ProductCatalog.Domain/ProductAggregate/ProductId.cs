using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class ProductId : Id<Guid>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}