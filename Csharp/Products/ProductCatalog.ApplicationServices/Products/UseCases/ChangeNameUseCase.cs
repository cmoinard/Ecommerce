using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class ChangeNameUseCase : ProductUseCaseBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeNameUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task ChangeNameAsync(ProductId productId, ProductName name)
        {
            var product = await SafeGetProductAsync(productId);
            
            var nameAlreadyExists = await Repository.NameExistsAsync(name, productId);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(name);

            product.Name = name;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}