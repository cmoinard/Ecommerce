using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;

namespace ProductCatalog.ApplicationServices.Products
{
    public interface IProductsRepository
    {
        Task<IReadOnlyCollection<Product>> GetAsync();
        Task<Product?> GetByIdAsync(ProductId productId);
        Task<bool> NameExistsAsync(ProductName name);
        Task<bool> NameExistsAsync(ProductName name, ProductId exceptProductId);

        Task CreateAsync(UncreatedProduct product);
        Task DeleteAsync(Product product);
    }
}