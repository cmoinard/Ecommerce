using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface IGetCategoriesUseCase
    {
        Task<IReadOnlyCollection<Category>> GetAllAsync();
    }
}