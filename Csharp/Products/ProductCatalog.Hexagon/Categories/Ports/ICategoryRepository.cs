using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;

namespace ProductCatalog.Hexagon.Categories.Ports
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(CategoryId id);
    }
}