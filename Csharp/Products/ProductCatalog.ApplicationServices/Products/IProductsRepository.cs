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
        Task<bool> NameExistsAsync(NonEmptyString name);
        Task<bool> NameExistsAsync(NonEmptyString name, ProductId exceptProductId);

        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}