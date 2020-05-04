using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.Domain.ProductAggregate;
using ProductCatalog.Infra.Sql.Models;
using Shared.Core;

namespace ProductCatalog.Infra.Sql
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

        public Task<bool> NameExistsAsync(NonEmptyString name) => 
            Set.AnyAsync(p => p.Name == (string)name);

        public Task<bool> NameExistsAsync(NonEmptyString name, ProductId exceptProductId) => 
            Set.AnyAsync(p => p.Name == (string)name && p.Id != (Guid)exceptProductId);

        public async Task CreateAsync(UncreatedProduct product)
        {
            var dbProduct = DbProduct.FromDomain(product);
            await Set.AddAsync(dbProduct);
        }

        public async Task DeleteAsync(Product product)
        {
            var dbProduct = await Set.FirstOrDefaultAsync(p => p.Id == (Guid) product.Id);
            if (dbProduct != null)
            {
                Set.Remove(dbProduct);
            }
        }
    }
}