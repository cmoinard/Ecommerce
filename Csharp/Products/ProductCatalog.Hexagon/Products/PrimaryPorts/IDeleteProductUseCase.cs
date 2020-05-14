using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IDeleteProductUseCase
    {
        Task DeleteAsync(ProductId id);
    }
}