using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.CategoryAggregate
{
    public class Category : AggregateRoot<CategoryId>
    {
        public Category(CategoryId id, CategoryName name)
            : base(id)
        {
            Name = name;
        }

        public CategoryName Name { get; }
    }
}