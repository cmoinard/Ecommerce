using System.Threading.Tasks;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.Ports;
using Shared.Core.Exceptions;

namespace ProductCatalog.Domain.Categories.UseCases
{
    public class CreateCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public CreateCategoryUseCase(
            ICategoriesRepository repository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
        {
            _repository = repository;
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }

        public async Task CreateAsync(UncreatedCategory category)
        {
            var nameAlreadyExists = await _repository.NameExistsAsync(category.Name);
            if (nameAlreadyExists)
                throw new CategoryNameAlreadyExistsException(category.Name);

            await _repository.CreateAsync(category);
            await _productCatalogUnitOfWork.SaveChangesAsync();
        }
    }

    public class CategoryNameAlreadyExistsException : DomainException
    {
        public CategoryNameAlreadyExistsException(CategoryName name)
        {
            Name = name;
        }

        public CategoryName Name { get; }
    }
}