using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Hexagon.Categories.SecondaryPorts
{
    public interface ICategoriesRepository
    {
        Task<IReadOnlyCollection<string>> GetAllAsync();
        Task<bool> ExistsAsync(string categoryId);
        Task DeleteAsync(string categoryId);
    }
}