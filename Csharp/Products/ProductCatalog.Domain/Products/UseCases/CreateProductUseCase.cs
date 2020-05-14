using System.Threading.Tasks;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;
using Shared.Core.Exceptions;

namespace ProductCatalog.Domain.Products.UseCases
{
    public class CreateProductUseCase
    {
        private readonly IProductsRepository _repository;
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public CreateProductUseCase(
            IProductsRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
        {
            _repository = repository;
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }
        
        public async Task CreateAsync(UncreatedProduct product)
        {
            var nameAlreadyExists = await _repository.NameExistsAsync(product.Name);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(product.Name);

            await _repository.CreateAsync(product);
            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }

    public class ProductNameAlreadyExistsException : DomainException
    {
        public ProductNameAlreadyExistsException(ProductName name)
        {
            Name = name;
        }

        public ProductName Name { get; }
    }
}