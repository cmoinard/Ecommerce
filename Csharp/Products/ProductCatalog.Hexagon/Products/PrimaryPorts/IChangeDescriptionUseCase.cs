using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeDescriptionUseCase
    {
        Task ChangeDescriptionAsync(ProductId productId, ProductDescription description);
    }
}