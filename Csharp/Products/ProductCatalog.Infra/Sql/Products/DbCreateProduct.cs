using System.Threading.Tasks;
using ProductCatalog.Hexagon.Products;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Products
{
    public class DbCreateProduct : ICreateProduct
    {
        private readonly ProductCatalogContext _context;

        public DbCreateProduct(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(UncreatedProduct product)
        {
            var dbProduct = DbProduct.FromDomain(product);
            
            await _context.Set<DbProduct>().AddAsync(dbProduct);
            await _context.SaveChangesAsync();
            
            return dbProduct.ToDomain();
        }
    }
}