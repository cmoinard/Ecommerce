using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Domain.ProductAggregate;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class GetProductsUseCase : ProductUseCaseBase
    {
        private readonly IProductsRepository _repository;

        public GetProductsUseCase(IProductsRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        public Task<IReadOnlyCollection<Product>> GetAsync() =>
            _repository.GetAsync();

        public Task<Product> GetByIdAsync(ProductId id) =>
            SafeGetProductAsync(id);
    }
}