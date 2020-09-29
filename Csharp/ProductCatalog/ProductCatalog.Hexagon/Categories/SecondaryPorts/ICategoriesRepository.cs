using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Hexagon.Categories.SecondaryPorts
{
    public interface ICategoriesRepository
    {
        Task<IReadOnlyCollection<string>> GetAllAsync();
    }
}