using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeNameUseCase
    {
        Task ChangeNameAsync(ProductId productId, ProductName name);
    }
}