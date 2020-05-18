using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Configurations
{
    public class DbCategoryConfiguration : IEntityTypeConfiguration<DbCategory>
    {
        public void Configure(EntityTypeBuilder<DbCategory> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();

            builder
                .HasMany<DbProductCategory>()
                .WithOne()
                .HasForeignKey(c => c.CategoryId);
        }
    }
}