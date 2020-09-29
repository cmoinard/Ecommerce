using ProductCatalog.Hexagon.Categories;

namespace ProductCatalog.Web.Categories
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
        
        public UncreatedCategory ToDomain() =>
            new UncreatedCategory(
                new CategoryName(CategoryName));
    }
}