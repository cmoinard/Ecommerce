using System.Threading.Tasks;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface IDeleteCategoryUseCase
    {
        Task DeleteAsync(string categoryId);
    }
}