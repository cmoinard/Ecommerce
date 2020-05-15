using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Products
{
    public class DbProductsRepository : IProductsRepository
    {
        private readonly ProductCatalogContext _context;

        public DbProductsRepository(ProductCatalogContext context)
        {
            _context = context;
        }

        private DbSet<DbProduct> Set => _context.Set<DbProduct>();
        
        public async Task<IReadOnlyCollection<Product>> GetAsync()
        {
            var dbProducts = await Set.Include(p => p.Categories).ToListAsync();

            return
                dbProducts
                    .Select(p => p.ToDomain())
                    .ToList();
        }

        public async Task<Product?> GetByIdAsync(ProductId productId)
        {
            var dbProduct = await Set.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == (Guid)productId);
            return dbProduct?.ToDomain();
        }

        public Task<bool> NameExistsAsync(ProductName name) => 
            Set.AnyAsync(p => p.Name == (string)name);

        public Task<bool> NameExistsAsync(ProductName name, ProductId exceptProductId) => 
            Set.AnyAsync(p => p.Name == (string)name && p.Id != (Guid)exceptProductId);
        
    }
}