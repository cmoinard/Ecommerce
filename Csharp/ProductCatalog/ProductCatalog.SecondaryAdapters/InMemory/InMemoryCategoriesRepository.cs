using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.SecondaryAdapters.InMemory.IdGenerators;
using Shared.Core;

namespace ProductCatalog.SecondaryAdapters.InMemory
{
    public class InMemoryCategoriesRepository : ICategoriesRepository
    {
        private readonly IntIdGenerator _idGenerator = new IntIdGenerator();
        
        private readonly List<Category> _categories = new List<Category>
        {
            new Category(new CategoryId(1), new CategoryName("claviers")), 
            new Category(new CategoryId(2), new CategoryName("souris"))
        };
        
        public async Task<IReadOnlyCollection<Category>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _categories;
        }

        public async Task<bool> ExistsAsync(CategoryId categoryId)
        {
            await Task.CompletedTask;
            return _categories.Any(c => c.Id == categoryId);
        }

        public async Task DeleteAsync(CategoryId categoryId)
        {
            await Task.CompletedTask;
            _categories.RemoveAll(c => c.Id == categoryId);
        }

        public async Task<bool> NameAlreadyExistsAsync(CategoryName categoryName)
        {
            await Task.CompletedTask;
            return _categories.Any(c => c.Name == categoryName);
        }

        public async Task<CategoryId> CreateAsync(UncreatedCategory category)
        {
            await Task.CompletedTask;
            var categoryId = new CategoryId(_idGenerator.NewId());

            _categories.Add(new Category(categoryId, category.Name));
            
            return categoryId;
        }

        public async Task<IReadOnlyCollection<CategoryId>> GetNonExistentIdsAsync(NonEmptyList<CategoryId> categoryIds)
        {
            await Task.CompletedTask;
            return
                categoryIds
                    .Except(_categories.Select(c => c.Id))
                    .ToList();
        }
    }
}