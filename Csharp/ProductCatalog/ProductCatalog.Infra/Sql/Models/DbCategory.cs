using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Infra.Sql.Models
{
    public class DbCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public Category ToDomain() =>
            new Category(new CategoryId(Id), new CategoryName(Name));

        public static DbCategory FromDomain(UncreatedCategory category) =>
            new DbCategory
            {
                Name = (string) category.Name
            };
    }
}