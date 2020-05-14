using System.Linq;
using ProductCatalog.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.DomainModeling;
using Shared.Core.Extensions;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Product : AggregateRoot<ProductId>
    {
        private NonEmptyList<CategoryId> _categoryIds = default!;
        
        public Product(
            ProductId id,
            ProductName name,
            ProductDescription description,
            Dimension dimension,
            Weight weight,
            NonEmptyList<CategoryId> categoryIds)
            : base(id)
        {
            Name = name;
            Description = description;
            Dimension = dimension;
            Weight = weight;
            CategoryIds = categoryIds;
        }
        
        public ProductName Name { get; set; }
        public ProductDescription Description { get; set; }
        public Dimension Dimension { get; set; }
        public Weight Weight { get; set; }

        public NonEmptyList<CategoryId> CategoryIds
        {
            get => _categoryIds;
            set =>
                _categoryIds =
                    value
                        .Distinct()
                        .ToNonEmptyList();
        }
    }
    
    
}