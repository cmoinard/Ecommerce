using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Exceptions;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class CreateProductUseCase
    {
        private readonly IProductsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task CreateAsync(UncreatedProduct product)
        {
            var nameAlreadyExists = await _repository.NameExistsAsync(product.Name);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(product.Name);

            await _repository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();
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