using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Infra.Sql.Models;

namespace ProductCatalog.Infra.Sql.Configurations
{
    public class DbProductConfiguration : IEntityTypeConfiguration<DbProduct>
    {
        public void Configure(EntityTypeBuilder<DbProduct> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Length).IsRequired();
            builder.Property(p => p.Width).IsRequired();
            builder.Property(p => p.Height).IsRequired();
            builder.Property(p => p.Weight).IsRequired();

            builder
                .HasMany(p => p.Categories)
                .WithOne()
                .HasForeignKey(c => c.ProductId);
        }
    }
}