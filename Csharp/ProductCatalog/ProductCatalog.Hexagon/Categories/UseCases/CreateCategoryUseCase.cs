using System;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using Shared.Core.Extensions;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class CreateCategoryUseCase : ICreateCategoryUseCase
    {
        private readonly ICategoriesRepository _repository;

        public CreateCategoryUseCase(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryId> CreateAsync(string categoryName)
        {
            var validation = CategoryName.TryCreate(categoryName);
            
            if (await _repository.NameAlreadyExistsAsync(validation.Value))
            {
                throw new CategoryNameAlreadyExistsException(categoryName);
            }
            
            var categoryId = await _repository.CreateAsync(validation.Value);
            return categoryId;
        }
    }
}