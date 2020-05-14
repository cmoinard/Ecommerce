using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;

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
        
        public async Task ChangeDescriptionAsync(ProductId productId, ProductDescription description)
        {
            var product = await SafeGetProductAsync(productId);

            product.Description = description;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}