using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeWeightUseCase
    {
        Task ChangeWeightAsync(ProductId productId, Weight weight);
    }
}