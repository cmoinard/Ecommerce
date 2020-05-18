using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace Pricing.Hexagon.Products.PrimaryPorts
{
    public interface ISetProductPriceUseCase
    {
        Task SetPriceAsync(ProductId id, Price price);
    }
}