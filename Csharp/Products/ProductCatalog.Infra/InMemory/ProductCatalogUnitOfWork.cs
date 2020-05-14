using System.Threading.Tasks;
using ProductCatalog.Domain;

namespace ProductCatalog.Infra.InMemory
{
    public class InMemoryProductCatalogUnitOfWork : IProductCatalogUnitOfWork
    {
        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}