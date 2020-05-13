using ProductCatalog.Domain.CategoryAggregate;

namespace ProductCatalog.ApplicationServices.Categories
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