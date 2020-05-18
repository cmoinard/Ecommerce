using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeWeightUseCase : ProductUseCaseBase, IChangeWeightUseCase
    {
        private readonly ISaveProduct _saveProduct;

        public ChangeWeightUseCase(
            IProductsRepository repository,
            ISaveProduct saveProduct)
            : base(repository)
        {
            _saveProduct = saveProduct;
        }

        public async Task ChangeWeightAsync(ProductId productId, Weight weight)
        {
            var product = await SafeGetProductAsync(productId);

            product.Weight = weight;

            await _saveProduct.SaveAsync(product);
        }
    }
}