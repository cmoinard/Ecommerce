using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.Ports;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeNameUseCase : ProductUseCaseBase
    {
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public ChangeNameUseCase(
            IProductsRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
            : base(repository)
        {
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }
        
        public async Task ChangeNameAsync(ProductId productId, ProductName name)
        {
            var product = await SafeGetProductAsync(productId);
            
            var nameAlreadyExists = await Repository.NameExistsAsync(name, productId);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(name);

            product.Name = name;

            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }
}