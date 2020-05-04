using System;
using System.Collections.Generic;
using System.Linq;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
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
                new NonEmptyString(Name),
                new NonEmptyString(Description),
                new Dimension(
                    Size.Cm(Length),
                    Size.Cm(Width),
                    Size.Cm(Height)),
                Domain.ProductAggregate.Weight.Grams(Weight),
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