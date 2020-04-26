using System.Threading.Tasks;
using ProductCatalog.Domain.CategoryAggregate;
using Shared.Core.Exceptions;

namespace ProductCatalog.ApplicationServices.Categories.UseCases
{
    public class DeleteCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryUseCase(
            ICategoriesRepository repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task DeleteAsync(CategoryId id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException<CategoryId>(id);

            await _repository.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}