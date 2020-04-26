using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class DeleteProductUseCase : ProductUseCaseBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductUseCase(
            IProductsRepository repository,
            IUnitOfWork unitOfWork)
            : base(repository)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(ProductId id)
        {
            var product = await SafeGetProductAsync(id);

            await Repository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}