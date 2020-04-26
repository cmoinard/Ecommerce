using System.Threading.Tasks;

namespace ProductCatalog.ApplicationServices
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}