using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;

namespace ProductCatalog.SecondaryAdapters
{
    public class InMemoryCategoriesRepository : ICategoriesRepository
    {
        private readonly List<Category> _categories = new List<Category>
        {
            new Category(new CategoryId(Guid.NewGuid()), new CategoryName("claviers")), 
            new Category(new CategoryId(Guid.NewGuid()), new CategoryName("souris"))
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
            var categoryId = new CategoryId(Guid.NewGuid());

            _categories.Add(new Category(categoryId, category.Name));
            
            return categoryId;
        }
    }
}