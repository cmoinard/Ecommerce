using System.Threading.Tasks;
using ProductCatalog.Hexagon;

namespace ProductCatalog.Infra.Sql
{
    public class DbProductCatalogUnitOfWork : IProductCatalogUnitOfWork
    {
        private readonly ProductCatalogContext _context;

        public DbProductCatalogUnitOfWork(ProductCatalogContext context)
        {
            _context = context;
        }
        
        public Task SaveChangesAsync() => 
            _context.SaveChangesAsync();
    }
}