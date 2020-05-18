using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface ICreateCategoryUseCase
    {
        Task<Category> CreateAsync(UncreatedCategory category);
    }
}