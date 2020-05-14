using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IGetProductsUseCase
    {
        Task<IReadOnlyCollection<Product>> GetAsync();
        Task<Product> GetByIdAsync(ProductId id);
    }
}