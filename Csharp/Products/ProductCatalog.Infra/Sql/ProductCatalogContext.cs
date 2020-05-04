using Microsoft.EntityFrameworkCore;
using ProductCatalog.Infra.Sql.Configurations;

namespace ProductCatalog.Infra.Sql
{
    public class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DbCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DbProductConfiguration());
            modelBuilder.ApplyConfiguration(new DbProductCategoryConfiguration());
        }
    }
}