using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Core;

namespace ProductCatalog.Hexagon.Categories.Ports
{
    public interface ICategoriesRepository : ICategoryRepository
    {
        Task<IReadOnlyCollection<Category>> GetAsync();

        Task<bool> NameExistsAsync(CategoryName name);

        Task CreateAsync(UncreatedCategory category);
        Task DeleteAsync(Category category);
        Task<IReadOnlyCollection<CategoryId>> GetNonExistentIdsAsync(NonEmptyList<CategoryId> ids);
    }
}