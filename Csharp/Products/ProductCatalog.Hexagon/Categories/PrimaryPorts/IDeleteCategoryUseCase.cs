using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface IDeleteCategoryUseCase
    {
        Task DeleteAsync(CategoryId id);
    }
}