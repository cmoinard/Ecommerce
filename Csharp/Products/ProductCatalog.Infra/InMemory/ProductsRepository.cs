using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.Domain.ProductAggregate;
using ProductCatalog.Infra.InMemory.IdGenerators;
using Shared.Core;

namespace ProductCatalog.Infra.InMemory
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly GuidIdGenerator _idGenerator = new GuidIdGenerator();
        private readonly List<Product> _products = new List<Product>();
        
        public async Task<IReadOnlyCollection<Product>> GetAsync()
        {
            await Task.CompletedTask;
            return _products;
        }

        public async Task<Product?> GetByIdAsync(ProductId productId)
        {
            await Task.CompletedTask;
            return _products.SingleOrDefault(p => p.Id == productId);
        }

        public async Task<bool> NameExistsAsync(NonEmptyString name)
        {
            await Task.CompletedTask;
            return _products.Any(p => p.Name == name);
        }

        public async Task<bool> NameExistsAsync(NonEmptyString name, ProductId exceptProductId)
        {
            await Task.CompletedTask;
            return _products.Any(p => p.Name == name && p.Id != exceptProductId);
        }

        public async Task CreateAsync(Product product)
        {
            await Task.CompletedTask;
            
            var newProduct =
                new Product(
                    new ProductId(_idGenerator.NewId()),
                    product.Name,
                    product.Description,
                    product.Dimension,
                    product.Weight,
                    product.CategoryIds);
            
            _products.Add(newProduct);
        }

        public async Task DeleteAsync(Product product)
        {
            await Task.CompletedTask;
            _products.Remove(product);
        }
    }
}