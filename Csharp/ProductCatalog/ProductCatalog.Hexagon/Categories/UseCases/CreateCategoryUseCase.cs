using System;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using Shared.Core.Extensions;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;

        public CreateCategoryUseCase(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryId> CreateAsync(UncreatedCategory category)
        {
            if (await _repository.NameAlreadyExistsAsync(category.Name))
            {
                throw new CategoryNameAlreadyExistsException(category.Name);
            }
            
            var categoryId = await _repository.CreateAsync(category);
            return categoryId;
        }
    }
}