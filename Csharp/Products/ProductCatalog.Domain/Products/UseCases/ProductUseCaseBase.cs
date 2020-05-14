using System.Threading.Tasks;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;
using Shared.Core.Exceptions;

namespace ProductCatalog.Domain.Products.UseCases
{
    public abstract class ProductUseCaseBase
    {
        protected ProductUseCaseBase(IProductsRepository repository)
        {
            Repository = repository;
        }

        protected IProductsRepository Repository { get; }

        protected async Task<Product> SafeGetProductAsync(ProductId productId)
        {
            var product = await Repository.GetByIdAsync(productId);
            if (product == null)
                throw new NotFoundException<ProductId>(productId);
            return product;
        }
    }
}