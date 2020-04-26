using System.Threading.Tasks;
using ProductCatalog.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

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

        public async Task CreateAsync(UnvalidatedCategoryState categoryState)
        {
            categoryState.EnsureIsValid();
            
            var name = await GetValidatedNameAsync(categoryState);

            await _repository.CreateAsync(new Category(name));
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<NonEmptyString> GetValidatedNameAsync(UnvalidatedCategoryState categoryState)
        {
            var name = categoryState.Name.ToNonEmpty();
            var nameAlreadyExists = await _repository.NameExistsAsync(name);
            if (nameAlreadyExists)
                throw new CategoryNameAlreadyExistsException(name);
            return name;
        }
    }

    public class CategoryNameAlreadyExistsException : DomainException
    {
        public CategoryNameAlreadyExistsException(NonEmptyString name)
        {
            Name = name;
        }

        public NonEmptyString Name { get; }
    }
}