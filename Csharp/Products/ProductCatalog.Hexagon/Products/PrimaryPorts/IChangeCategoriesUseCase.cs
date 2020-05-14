using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeCategoriesUseCase
    {
        Task ChangeCategoriesAsync(ProductId productId, NonEmptyList<CategoryId> categoryIds);
    }
}