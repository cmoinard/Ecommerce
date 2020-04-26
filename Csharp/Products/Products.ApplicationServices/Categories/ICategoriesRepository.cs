using System.Collections.Generic;
using System.Threading.Tasks;
using Products.Domain.CategoryAggregate;
using Shared.Core;

namespace Products.ApplicationServices.Categories
{
    public interface ICategoriesRepository
    {
        Task<IReadOnlyCollection<Category>> GetAsync();
        Task<Category?> GetByIdAsync(Category.Id id);

        Task<bool> NameExistsAsync(NonEmptyString name);

        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}