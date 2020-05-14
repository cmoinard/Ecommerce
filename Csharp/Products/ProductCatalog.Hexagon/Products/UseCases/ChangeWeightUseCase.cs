using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.Ports;

namespace ProductCatalog.Hexagon.Products.UseCases
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