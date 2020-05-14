using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeDimensionUseCase : ProductUseCaseBase, IChangeDimensionUseCase
    {
        private readonly ISaveProduct _saveProduct;

        public ChangeDimensionUseCase(
            IProductsRepository repository,
            ISaveProduct saveProduct)
            : base(repository)
        {
            _saveProduct = saveProduct;
        }

        public async Task ChangeDimensionsAsync(ProductId productId, Dimension dimension)
        {
            var product = await SafeGetProductAsync(productId);

            product.Dimension = dimension;

            await _saveProduct.SaveAsync(product);
        }
    }
}