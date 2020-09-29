using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Hexagon.Categories
{
    public class CategoryId : Id<Guid>
    {
        public CategoryId(Guid internalValue) : base(internalValue)
        {
        }
    }
}