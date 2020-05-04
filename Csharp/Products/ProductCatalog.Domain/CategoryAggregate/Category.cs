using Shared.Core;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.CategoryAggregate
{
    public class Category : AggregateRoot<CategoryId>
    {
        public Category(CategoryId id, NonEmptyString name)
            : base(id)
        {
            Name = name;
        }

        public NonEmptyString Name { get; }
    }
}