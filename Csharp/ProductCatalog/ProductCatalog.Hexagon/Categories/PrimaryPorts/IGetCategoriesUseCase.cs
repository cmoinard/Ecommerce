using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface IGetCategoriesUseCase
    {
        Task<IReadOnlyCollection<Category>> GetAllAsync();
    }
}