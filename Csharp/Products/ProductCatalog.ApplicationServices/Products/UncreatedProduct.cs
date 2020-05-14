using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;

namespace ProductCatalog.ApplicationServices.Products
{
    public class UncreatedProduct
    {
        public UncreatedProduct(
            ProductName name,
            ProductDescription description,
            Dimension dimension,
            Weight weight,
            NonEmptyList<CategoryId> categoryIds)
        {
            Name = name;
            Description = description;
            Dimension = dimension;
            Weight = weight;
            CategoryIds = categoryIds;
        }

        public ProductName Name { get; }
        public ProductDescription Description { get; }
        public Dimension Dimension { get; }
        public Weight Weight { get; }
        public NonEmptyList<CategoryId> CategoryIds { get; }
    }
}