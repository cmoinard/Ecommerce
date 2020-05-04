using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;

namespace ProductCatalog.ApplicationServices.Products
{
    public class UncreatedProduct
    {
        public UncreatedProduct(
            NonEmptyString name,
            NonEmptyString description,
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

        public NonEmptyString Name { get; }
        public NonEmptyString Description { get; }
        public Dimension Dimension { get; }
        public Weight Weight { get; }
        public NonEmptyList<CategoryId> CategoryIds { get; }
    }
}