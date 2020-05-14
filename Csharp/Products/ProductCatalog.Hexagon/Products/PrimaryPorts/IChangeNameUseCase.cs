using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeNameUseCase
    {
        Task ChangeNameAsync(ProductId productId, ProductName name);
    }
}