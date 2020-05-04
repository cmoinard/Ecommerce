using System.Threading.Tasks;
using ProductCatalog.ApplicationServices;

namespace ProductCatalog.Infra.Sql
{
    public class DbUnitOfWork : IUnitOfWork
    {
        private readonly ProductCatalogContext _context;

        public DbUnitOfWork(ProductCatalogContext context)
        {
            _context = context;
        }
        
        public Task SaveChangesAsync() => 
            _context.SaveChangesAsync();
    }
}