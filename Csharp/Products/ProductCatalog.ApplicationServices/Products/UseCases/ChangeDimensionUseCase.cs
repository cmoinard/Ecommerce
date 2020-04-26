using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Validations;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class ChangeDimensionUseCase : ProductUseCaseBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDimensionUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ChangeDimensionsAsync(ProductId productId, UnvalidatedDimensionState dimensionState)
        {
            var product = await SafeGetProductAsync(productId);
            
            dimensionState.EnsureIsValid();

            product.Dimension = dimensionState.ToDomain();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}