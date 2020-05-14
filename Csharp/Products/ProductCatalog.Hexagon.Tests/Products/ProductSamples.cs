using System;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core;
using Shared.Domain;

namespace ProductCatalog.Hexagon.Tests.Products
{
    public static class ProductSamples
    {
        public static Product TypeMatrix() =>
            new Product(
                new ProductId(Guid.NewGuid()), 
                new ProductName("TypeMatrix 2030 BÉPO"), 
                new ProductDescription("Best keyboard ever"),
                new Dimension(
                    Size.Cm(33),
                    Size.Cm(14),
                    Size.Cm(2)),
                Weight.Grams(709),
                new NonEmptyList<CategoryId>(new CategoryId(1)));
    }
}