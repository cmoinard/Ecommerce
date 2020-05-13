using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Domain.CategoryAggregate;
using Shared.Core;

namespace ProductCatalog.ApplicationServices.Categories
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