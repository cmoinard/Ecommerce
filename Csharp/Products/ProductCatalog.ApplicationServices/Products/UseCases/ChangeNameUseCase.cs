using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

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
        
        public async Task ChangeNameAsync(ProductId productId, string newName)
        {
            var product = await SafeGetProductAsync(productId);
            
            if (newName.IsNullOrWhiteSpace())
                throw new ValidationException(new EmptyProductNameValidationError());

            var validatedName = newName.ToNonEmpty();
            var nameAlreadyExists = await Repository.NameExistsAsync(validatedName, productId);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(validatedName);

            product.Name = validatedName;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}