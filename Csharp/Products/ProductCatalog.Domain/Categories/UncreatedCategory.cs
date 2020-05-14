using ProductCatalog.Domain.Categories.Aggregate;

namespace ProductCatalog.Domain.Categories
{
    public class UncreatedCategory
    {
        public UncreatedCategory(CategoryName name)
        {
            Name = name;
        }

        public CategoryName Name { get; }
    }
}