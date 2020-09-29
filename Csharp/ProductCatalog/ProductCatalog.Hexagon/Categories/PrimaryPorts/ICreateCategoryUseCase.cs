using System.Threading.Tasks;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface ICreateCategoryUseCase
    {
        Task<CategoryId> CreateAsync(UncreatedCategory categoryName);
    }
}