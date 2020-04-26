using System.Threading.Tasks;
using ProductCatalog.Domain.CategoryAggregate;

namespace ProductCatalog.ApplicationServices
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(CategoryId id);
    }
}