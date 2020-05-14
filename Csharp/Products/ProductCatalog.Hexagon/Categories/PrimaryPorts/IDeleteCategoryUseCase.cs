using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Categories.PrimaryPorts
{
    public interface IDeleteCategoryUseCase
    {
        Task DeleteAsync(CategoryId id);
    }
}