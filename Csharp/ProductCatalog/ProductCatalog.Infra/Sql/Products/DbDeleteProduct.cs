using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Products
{
    public class DbDeleteProduct : IDeleteProduct
    {
        private readonly ProductCatalogContext _context;

        public DbDeleteProduct(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(Product product)
        {
            var dbProduct = await _context.Set<DbProduct>().FirstOrDefaultAsync(p => p.Id == (Guid) product.Id);
            if (dbProduct != null)
            {
                _context.Set<DbProduct>().Remove(dbProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}