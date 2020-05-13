using System.Threading.Tasks;
using ProductCatalog.Domain.CategoryAggregate;
using Shared.Core.Exceptions;

namespace ProductCatalog.ApplicationServices.Categories.UseCases
{
    public class CreateCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryUseCase(
            ICategoriesRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(UncreatedCategory category)
        {
            var nameAlreadyExists = await _repository.NameExistsAsync(category.Name);
            if (nameAlreadyExists)
                throw new CategoryNameAlreadyExistsException(category.Name);

            await _repository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();
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