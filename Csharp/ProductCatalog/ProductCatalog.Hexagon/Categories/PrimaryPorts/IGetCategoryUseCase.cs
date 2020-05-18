using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface IGetCategoryUseCase
    {
        Task<Category> GetByIdAsync(CategoryId id);
        Task<IReadOnlyCollection<Category>> GetAsync();
    }
}