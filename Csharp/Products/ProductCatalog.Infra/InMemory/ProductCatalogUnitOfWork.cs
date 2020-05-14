using System.Threading.Tasks;
using ProductCatalog.Hexagon;

namespace ProductCatalog.Infra.InMemory
{
    public class InMemoryProductCatalogUnitOfWork : IProductCatalogUnitOfWork
    {
        public Task SaveChangesAsync() => Task.CompletedTask;
    }
}