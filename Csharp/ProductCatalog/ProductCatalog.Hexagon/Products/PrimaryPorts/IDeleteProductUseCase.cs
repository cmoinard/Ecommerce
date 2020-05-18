using System.Threading.Tasks;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IDeleteProductUseCase
    {
        Task DeleteAsync(ProductId id);
    }
}