using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Core;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Categories.SecondaryPorts
{
    public interface ICategoriesRepository
    {
        Task<Category?> GetByIdAsync(CategoryId id);

        Task<bool> NameExistsAsync(CategoryName name);
        Task<IReadOnlyCollection<Category>> GetAsync();
        Task<IReadOnlyCollection<CategoryId>> GetNonExistentIdsAsync(NonEmptyList<CategoryId> ids);
    }
}