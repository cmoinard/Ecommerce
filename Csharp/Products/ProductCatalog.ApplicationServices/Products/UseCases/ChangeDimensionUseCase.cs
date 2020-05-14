using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;

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

        public async Task ChangeDimensionsAsync(ProductId productId, Dimension dimension)
        {
            var product = await SafeGetProductAsync(productId);

            product.Dimension = dimension;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}