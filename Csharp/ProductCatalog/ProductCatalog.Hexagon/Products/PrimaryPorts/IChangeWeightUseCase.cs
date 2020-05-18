using System.Threading.Tasks;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IChangeWeightUseCase
    {
        Task ChangeWeightAsync(ProductId productId, Weight weight);
    }
}