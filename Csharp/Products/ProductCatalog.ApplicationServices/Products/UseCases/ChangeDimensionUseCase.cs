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

        public async Task ChangeDimensionsAsync(ProductId productId, UnvalidatedDimension dimension)
        {
            var product = await SafeGetProductAsync(productId);
            
            dimension.EnsureIsValid();

            product.Dimension = dimension.ToDomain();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}