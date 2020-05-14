using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeDescriptionUseCase
    {
        Task ChangeDescriptionAsync(ProductId productId, ProductDescription description);
    }
}