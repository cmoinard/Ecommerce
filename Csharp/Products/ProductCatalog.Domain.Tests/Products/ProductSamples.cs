using System;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Products.Aggregate;
using Shared.Core;

namespace ProductCatalog.Domain.Tests.Products
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