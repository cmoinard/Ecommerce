using System.Threading.Tasks;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;

namespace ProductCatalog.Domain.Products.UseCases
{
    public class ChangeDimensionUseCase : ProductUseCaseBase
    {
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public ChangeDimensionUseCase(
            IProductsRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
            : base(repository)
        {
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }

        public async Task ChangeDimensionsAsync(ProductId productId, Dimension dimension)
        {
            var product = await SafeGetProductAsync(productId);

            product.Dimension = dimension;

            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }
}