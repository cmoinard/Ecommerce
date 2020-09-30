using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core;

namespace ProductCatalog.Hexagon.Products.SecondaryPorts
{
    public interface IProductsRepository
    {
        Task<IReadOnlyCollection<Product>> GetAsync();
        Task<IReadOnlyCollection<Product>> GetAsync(NonEmptyList<ProductId> productIds);
        Task<Product?> GetByIdAsync(ProductId productId);
        Task<bool> NameExistsAsync(ProductName name);
        Task<bool> NameExistsAsync(ProductName name, ProductId exceptProductId);
    }
}