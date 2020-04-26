using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.CategoryAggregate
{
    public class CategoryId : SimpleValueObject<int>
    {
        public CategoryId(int value) 
            : base(value)
        {
        }
    }
}