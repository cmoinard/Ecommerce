using System.Threading.Tasks;
using ProductCatalog.Domain.Categories.Aggregate;

namespace ProductCatalog.Domain.Categories.Ports
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(CategoryId id);
    }
}