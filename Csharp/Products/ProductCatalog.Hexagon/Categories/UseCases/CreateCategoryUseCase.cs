using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;
        private readonly ICreateCategory _createCategory;

        public CreateCategoryUseCase(
            ICategoriesRepository repository,
            ICreateCategory createCategory)
        {
            _repository = repository;
            _createCategory = createCategory;
        }

        public async Task<Category> CreateAsync(UncreatedCategory category)
        {
            var nameAlreadyExists = await _repository.NameExistsAsync(category.Name);
            if (nameAlreadyExists)
                throw new CategoryNameAlreadyExistsException(category.Name);

            return await _createCategory.CreateAsync(category);
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