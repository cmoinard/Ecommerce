using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Configurations
{
    public class DbProductCategoryConfiguration : IEntityTypeConfiguration<DbProductCategory>
    {
        public void Configure(EntityTypeBuilder<DbProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(c => new {c.CategoryId, c.ProductId});
        }
    }
}