using System.Threading.Tasks;
using Products.Domain.CategoryAggregate;
using Shared.Core.Exceptions;

namespace Products.ApplicationServices.Categories.UseCases
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
        
        public async Task DeleteAsync(Category.Id id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException<Category.Id>(id);

            await _repository.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}