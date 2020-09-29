using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;

        public DeleteCategoryUseCase(ICategoriesRepository repository)
        {
            _repository = repository;
        }
        
        public async Task DeleteAsync(string categoryId)
        {
            var exists = await _repository.ExistsAsync(categoryId);
            if (!exists)
            {
                throw new NotFoundException<string>(categoryId);
            }

            await _repository.DeleteAsync(categoryId);
        }
    }
}