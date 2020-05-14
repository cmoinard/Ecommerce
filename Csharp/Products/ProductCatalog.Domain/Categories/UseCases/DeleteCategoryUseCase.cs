using System.Threading.Tasks;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.Ports;
using Shared.Core.Exceptions;

namespace ProductCatalog.Domain.Categories.UseCases
{
    public class DeleteCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public DeleteCategoryUseCase(
            ICategoriesRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
        {
            _repository = repository;
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }
        
        public async Task DeleteAsync(CategoryId id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException<CategoryId>(id);

            await _repository.DeleteAsync(category);
            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }
}