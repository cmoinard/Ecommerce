using System;
using Shared.Core.DomainModeling;

namespace Shared.Domain
{
    public class ProductId : Id<Guid>
    {
        public ProductId(Guid internalValue) : base(internalValue)
        {
        }
    }
}