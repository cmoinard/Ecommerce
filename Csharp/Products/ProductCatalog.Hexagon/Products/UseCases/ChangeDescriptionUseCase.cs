using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.Ports;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeDescriptionUseCase : ProductUseCaseBase
    {
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public ChangeDescriptionUseCase(
            IProductsRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
            : base(repository)
        {
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }
        
        public async Task ChangeDescriptionAsync(ProductId productId, ProductDescription description)
        {
            var product = await SafeGetProductAsync(productId);

            product.Description = description;

            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }
}