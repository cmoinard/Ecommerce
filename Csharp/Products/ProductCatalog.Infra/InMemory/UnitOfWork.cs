using System.Threading.Tasks;
using ProductCatalog.ApplicationServices;

namespace ProductCatalog.Infra.InMemory
{
    public class UnitOfWork : IUnitOfWork
    {
        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}