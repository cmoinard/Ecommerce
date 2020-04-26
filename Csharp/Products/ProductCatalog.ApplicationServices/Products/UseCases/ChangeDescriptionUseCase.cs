using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class ChangeDescriptionUseCase : ProductUseCaseBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDescriptionUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task ChangeDescriptionAsync(ProductId productId, string newDescription)
        {
            var product = await SafeGetProductAsync(productId);

            if (newDescription.IsNullOrWhiteSpace())
                throw new ValidationException(new EmptyProductDescriptionValidationError());

            product.Description = newDescription.ToNonEmpty();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}