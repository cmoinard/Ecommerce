using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using Shared.Core.Exceptions;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class GetCategoryUseCase : IGetCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;

        public GetCategoryUseCase(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Category> GetByIdAsync(CategoryId id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category ==  null)
                throw new NotFoundException<CategoryId>(id);

            return category;
        }

        public async Task<IReadOnlyCollection<Category>> GetAsync() => 
            await _repository.GetAsync();
    }
}