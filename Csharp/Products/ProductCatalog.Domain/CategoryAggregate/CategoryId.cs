using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.CategoryAggregate
{
    public class CategoryId : Id<int>
    {
        public CategoryId(int internalValue) 
            : base(internalValue)
        {
        }
    }
}