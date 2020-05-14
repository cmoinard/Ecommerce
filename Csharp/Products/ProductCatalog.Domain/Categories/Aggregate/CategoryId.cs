using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.Categories.Aggregate
{
    public class CategoryId : Id<int>
    {
        public CategoryId(int internalValue) 
            : base(internalValue)
        {
        }
    }
}