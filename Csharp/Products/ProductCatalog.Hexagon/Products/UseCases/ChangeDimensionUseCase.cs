using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.Ports;

namespace ProductCatalog.Hexagon.Products.UseCases
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