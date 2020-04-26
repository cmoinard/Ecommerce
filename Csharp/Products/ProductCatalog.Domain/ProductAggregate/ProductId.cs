using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class ProductId : SimpleValueObject<Guid>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}