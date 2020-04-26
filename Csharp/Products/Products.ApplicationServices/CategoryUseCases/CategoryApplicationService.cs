using System.Threading.Tasks;
using Products.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.Exceptions;
using Shared.Core.Validations;

namespace Products.ApplicationServices.CategoryUseCases
{
    public class CategoryApplicationService
    {
        private readonly ICategoriesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryApplicationService(
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
        
        public async Task DeleteAsync(Category.Id id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException<Category.Id>(id);

            await _repository.DeleteAsync(category);
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
    
    public class CategoryNameAlreadyExistsException : DomainException
    {
        public CategoryNameAlreadyExistsException(NonEmptyString name)
        {
            Name = name;
        }

        public NonEmptyString Name { get; }
    }
}