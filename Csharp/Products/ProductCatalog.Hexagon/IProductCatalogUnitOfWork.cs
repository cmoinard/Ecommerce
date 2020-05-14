using System.Threading.Tasks;

namespace ProductCatalog.Hexagon
{
    public interface IProductCatalogUnitOfWork
    {
        Task SaveChangesAsync();
    }
}