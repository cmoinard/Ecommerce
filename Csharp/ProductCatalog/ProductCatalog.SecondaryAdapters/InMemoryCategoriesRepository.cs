using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;

namespace ProductCatalog.SecondaryAdapters
{
    public class InMemoryCategoriesRepository : ICategoriesRepository
    {
        public async Task<IReadOnlyCollection<string>> GetAllAsync()
        {
            await Task.CompletedTask;
            return new[] { "claviers", "souris" };
        }
    }
}