using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using Shared.Core;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class GetProductsUseCase : ProductUseCaseBase, IGetProductsUseCase
    {
        private readonly IProductsRepository _repository;

        public GetProductsUseCase(IProductsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Task<IReadOnlyCollection<Product>> GetAsync() =>
            _repository.GetAsync();

        public Task<IReadOnlyCollection<Product>> GetAsync(NonEmptyList<ProductId> productIds) => 
            _repository.GetAsync(productIds);

        public Task<Product> GetByIdAsync(ProductId id) =>
            SafeGetProductAsync(id);
    }
}