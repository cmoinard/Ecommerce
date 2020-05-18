using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class DeleteProductUseCase : ProductUseCaseBase, IDeleteProductUseCase
    {
        private readonly IDeleteProduct _deleteProduct;

        public DeleteProductUseCase(
            IProductsRepository repository,
            IDeleteProduct deleteProduct)
            : base(repository)
        {
            _deleteProduct = deleteProduct;
        }

        public async Task DeleteAsync(ProductId id)
        {
            var product = await SafeGetProductAsync(id);

            await _deleteProduct.DeleteAsync(product);
        }
    }
}