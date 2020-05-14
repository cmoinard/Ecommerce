using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;
        private readonly IDeleteCategory _deleteCategory;

        public DeleteCategoryUseCase(
            ICategoriesRepository repository,
            IDeleteCategory deleteCategory)
        {
            _repository = repository;
            _deleteCategory = deleteCategory;
        }
        
        public async Task DeleteAsync(CategoryId id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException<CategoryId>(id);

            await _deleteCategory.DeleteAsync(category);
        }
    }
}