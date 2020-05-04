using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductCatalog.Infra.Sql
{
    public class ProductCatalogContextFactory : IDesignTimeDbContextFactory<ProductCatalogContext>
    {
        public ProductCatalogContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        @"..\ProductCatalog.Web"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Hexagonal");
            var optionsBuilder = new DbContextOptionsBuilder<ProductCatalogContext>();
            optionsBuilder.UseSqlServer(connectionString);
            
            return new ProductCatalogContext(optionsBuilder.Options);
        }
    }
}