using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.Ports;
using Shared.Core.Exceptions;

namespace ProductCatalog.Domain.Categories.UseCases
{
    public class GetCategoryUseCase
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