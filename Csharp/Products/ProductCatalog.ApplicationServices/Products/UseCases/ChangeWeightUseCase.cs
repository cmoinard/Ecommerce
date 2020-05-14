using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;

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

        public async Task ChangeWeightAsync(ProductId productId, Weight weight)
        {
            var product = await SafeGetProductAsync(productId);

            product.Weight = weight;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}