using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.Ports;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class DeleteProductUseCase : ProductUseCaseBase
    {
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public DeleteProductUseCase(
            IProductsRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
            : base(repository)
        {
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }

        public async Task DeleteAsync(ProductId id)
        {
            var product = await SafeGetProductAsync(id);

            await Repository.DeleteAsync(product);
            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }
}