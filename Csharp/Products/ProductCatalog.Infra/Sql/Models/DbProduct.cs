using System;
using System.Collections.Generic;
using System.Linq;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Products;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core.Extensions;

namespace ProductCatalog.Infra.Sql.Models
{
    public class DbProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<DbProductCategory> Categories { get; set; }

        public Product ToDomain() =>
            new Product(
                new ProductId(Id),
                new ProductName(Name),
                new ProductDescription(Description), 
                new Dimension(
                    Size.Cm(Length),
                    Size.Cm(Width),
                    Size.Cm(Height)),
                Hexagon.Products.Aggregate.Weight.Grams(Weight),
                Categories
                    .Select(c => new CategoryId(c.CategoryId))
                    .ToNonEmptyList()
            );

        public static DbProduct FromDomain(UncreatedProduct product) =>
            new DbProduct
            {
                Name = (string) product.Name,
                Description = (string) product.Description,
                Length = product.Dimension.Length.ToCm(),
                Width = product.Dimension.Width.ToCm(),
                Height = product.Dimension.Height.ToCm(),
                Weight = product.Weight.ToGrams(),
                Categories =
                    product.CategoryIds
                        .Select(cId => new DbProductCategory {CategoryId = (int) cId })
                        .ToList()
            };
    }
}