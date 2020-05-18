using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;

namespace ProductCatalog.Hexagon.Categories.SecondaryPorts
{
    public interface IDeleteCategory
    {
        Task DeleteAsync(Category category);
    }
}