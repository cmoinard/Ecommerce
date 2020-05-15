using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;

namespace ProductCatalog.Infra.Sql.Products
{
    public class DbSaveProduct : ISaveProduct
    {
        private readonly ProductCatalogContext _context;

        public DbSaveProduct(ProductCatalogContext context)
        {
            _context = context;
        }
        
        public Task SaveAsync(Product product) => 
            _context.SaveChangesAsync();
    }
}