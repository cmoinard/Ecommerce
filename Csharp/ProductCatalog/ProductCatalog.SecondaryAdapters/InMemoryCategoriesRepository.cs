using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;

namespace ProductCatalog.SecondaryAdapters
{
    public class InMemoryCategoriesRepository : ICategoriesRepository
    {
        private readonly List<string> _categories = new List<string>
        {
            "claviers", 
            "souris" 
        };
        
        public async Task<IReadOnlyCollection<string>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _categories;
        }

        public async Task<bool> ExistsAsync(string categoryId)
        {
            await Task.CompletedTask;
            return _categories.Contains(categoryId);
        }

        public async Task DeleteAsync(string categoryId)
        {
            await Task.CompletedTask;
            _categories.Remove(categoryId);
        }
    }
}