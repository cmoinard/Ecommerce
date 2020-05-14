using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Domain.Categories;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.Ports;
using ProductCatalog.Infra.InMemory.IdGenerators;
using Shared.Core;
using Shared.Core.Extensions;

namespace ProductCatalog.Infra.InMemory
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly List<Category> _categories = new List<Category>();
        private readonly IntIdGenerator _idGenerator = new IntIdGenerator();

        public async Task<Category?> GetByIdAsync(CategoryId id)
        {
            await Task.CompletedTask;
            return _categories.SingleOrDefault(c => c.Id == id);
        }

        public async Task<IReadOnlyCollection<Category>> GetAsync()
        {
            await Task.CompletedTask;
            return _categories;
        }

        public async Task<bool> NameExistsAsync(CategoryName name)
        {
            await Task.CompletedTask;
            return _categories.Any(c => c.Name == name);
        }

        public async Task CreateAsync(UncreatedCategory category)
        {
            await Task.CompletedTask;
            
            var newCategory =
                new Category(
                    new CategoryId(_idGenerator.NewId()),
                    category.Name);
            _categories.Add(newCategory);
        }

        public async Task DeleteAsync(Category category)
        {
            await Task.CompletedTask;
            _categories.Remove(category);
        }

        public async Task<IReadOnlyCollection<CategoryId>> GetNonExistentIdsAsync(NonEmptyList<CategoryId> ids)
        {
            await Task.CompletedTask;
            return ids.Where(id => _categories.None(c => c.Id == id)).ToList();
        }
    }
}