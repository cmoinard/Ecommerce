using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;

namespace ProductCatalog.Hexagon.Categories.SecondaryPorts
{
    public interface ICreateCategory
    {
        Task<Category> CreateAsync(UncreatedCategory category);
    }
}