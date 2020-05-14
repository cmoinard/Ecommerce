using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeDescriptionUseCase : ProductUseCaseBase, IChangeDescriptionUseCase
    {
        private readonly ISaveProduct _saveProduct;

        public ChangeDescriptionUseCase(
            IProductsRepository repository,
            ISaveProduct saveProduct)
            : base(repository)
        {
            _saveProduct = saveProduct;
        }
        
        public async Task ChangeDescriptionAsync(ProductId productId, ProductDescription description)
        {
            var product = await SafeGetProductAsync(productId);

            product.Description = description;

            await _saveProduct.SaveAsync(product);
        }
    }
}