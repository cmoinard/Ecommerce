using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class CreateProductUseCase : ICreateProductUseCase
    {
        private readonly IProductsRepository _repository;
        private readonly ICreateProduct _createProduct;

        public CreateProductUseCase(
            IProductsRepository repository,
            ICreateProduct createProduct)
        {
            _repository = repository;
            _createProduct = createProduct;
        }
        
        public async Task<ProductId> CreateAsync(UncreatedProduct product)
        {
            var nameAlreadyExists = await _repository.NameExistsAsync(product.Name);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(product.Name);

            return await _createProduct.CreateAsync(product);
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