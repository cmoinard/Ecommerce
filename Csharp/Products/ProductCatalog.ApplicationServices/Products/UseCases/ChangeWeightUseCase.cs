using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Exceptions;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class ChangeWeightUseCase : ProductUseCaseBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeWeightUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ChangeWeightAsync(ProductId productId, int weightInGrams)
        {
            var product = await SafeGetProductAsync(productId);
            
            if (weightInGrams < 0)
                throw new ValidationException(new NegativeWeightValidationError());

            product.Weight = Weight.Grams(weightInGrams);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}