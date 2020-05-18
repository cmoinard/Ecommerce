using System.Threading.Tasks;
using Shared.Core;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeCategoriesUseCase
    {
        Task ChangeCategoriesAsync(ProductId productId, NonEmptyList<CategoryId> categoryIds);
    }
}