using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.SecondaryPorts
{
    public interface IProductsRepository
    {
        Task<IReadOnlyCollection<Product>> GetAsync();
        Task<Product?> GetByIdAsync(ProductId productId);
        Task<bool> NameExistsAsync(ProductName name);
        Task<bool> NameExistsAsync(ProductName name, ProductId exceptProductId);
    }
}