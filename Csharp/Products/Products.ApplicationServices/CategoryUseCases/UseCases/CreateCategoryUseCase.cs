using System.Threading.Tasks;
using Products.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.Validations;

namespace Products.ApplicationServices.CategoryUseCases.UseCases
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
            var name = new NonEmptyString(categoryState.Name);
            var nameAlreadyExists = await _repository.NameExistsAsync(name);
            if (nameAlreadyExists)
                throw new CategoryNameAlreadyExistsException(name);
            return name;
        }
    }
}