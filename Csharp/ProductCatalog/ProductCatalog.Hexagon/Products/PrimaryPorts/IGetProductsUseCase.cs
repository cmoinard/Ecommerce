using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core;

namespace ProductCatalog.Hexagon.Products.PrimaryPorts
{
    public interface IGetProductsUseCase
    {
        Task<IReadOnlyCollection<Product>> GetAsync();
        Task<IReadOnlyCollection<Product>> GetAsync(NonEmptyList<ProductId> productIds);
        Task<Product> GetByIdAsync(ProductId id);
    }
}