using System.Threading.Tasks;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;

namespace ProductCatalog.Domain.Products.UseCases
{
    public class ChangeWeightUseCase : ProductUseCaseBase
    {
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public ChangeWeightUseCase(
            IProductsRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
            : base(repository)
        {
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }

        public async Task ChangeWeightAsync(ProductId productId, Weight weight)
        {
            var product = await SafeGetProductAsync(productId);

            product.Weight = weight;

            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }
}