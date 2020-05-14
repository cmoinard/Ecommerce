using System.Threading.Tasks;

namespace ProductCatalog.Domain
{
    public interface IProductCatalogUnitOfWork
    {
        Task SaveChangesAsync();
    }
}