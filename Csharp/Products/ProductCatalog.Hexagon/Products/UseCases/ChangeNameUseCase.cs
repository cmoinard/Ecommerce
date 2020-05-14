using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeNameUseCase : ProductUseCaseBase, IChangeNameUseCase
    {
        private readonly ISaveProduct _saveProduct;

        public ChangeNameUseCase(
            IProductsRepository repository,
            ISaveProduct saveProduct)
            : base(repository)
        {
            _saveProduct = saveProduct;
        }
        
        public async Task ChangeNameAsync(ProductId productId, ProductName name)
        {
            var product = await SafeGetProductAsync(productId);
            
            var nameAlreadyExists = await Repository.NameExistsAsync(name, productId);
            if (nameAlreadyExists)
                throw new ProductNameAlreadyExistsException(name);

            product.Name = name;

            await _saveProduct.SaveAsync(product);
        }
    }
}