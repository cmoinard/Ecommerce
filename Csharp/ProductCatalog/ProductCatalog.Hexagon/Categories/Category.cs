using Shared.Core.DomainModeling;

namespace ProductCatalog.Hexagon.Categories
{
    public class Category : AggregateRoot<CategoryId>
    {
        public Category(CategoryId id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; }
    }
}