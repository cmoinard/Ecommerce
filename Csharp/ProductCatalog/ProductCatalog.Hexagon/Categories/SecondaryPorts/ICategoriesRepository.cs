using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Core;

namespace ProductCatalog.Hexagon.Categories.SecondaryPorts
{
    public interface ICategoriesRepository
    {
        Task<IReadOnlyCollection<Category>> GetAllAsync();
        Task<bool> ExistsAsync(CategoryId categoryId);
        Task DeleteAsync(CategoryId categoryId);
        Task<bool> NameAlreadyExistsAsync(CategoryName categoryName);
        Task<CategoryId> CreateAsync(UncreatedCategory categoryName);
        Task<IReadOnlyCollection<CategoryId>> GetNonExistentIdsAsync(NonEmptyList<CategoryId> categoryIds);
    }
}