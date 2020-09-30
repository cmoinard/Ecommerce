using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeCategoriesUseCase
    {
        Task ChangeCategoriesAsync(ProductId productId, NonEmptyList<CategoryId> categoryIds);
    }
}