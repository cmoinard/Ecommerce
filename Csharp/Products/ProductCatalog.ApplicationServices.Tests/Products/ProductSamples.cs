using System;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
using Shared.Core.Extensions;

namespace ProductCatalog.ApplicationServices.Tests.Products
{
    public static class ProductSamples
    {
        public static Product TypeMatrix() =>
            new Product(
                new ProductId(Guid.NewGuid()), 
                "TypeMatrix 2030 BÉPO".ToNonEmpty(),
                "Best keyboard ever".ToNonEmpty(),
                new Dimension(
                    Size.Cm(33),
                    Size.Cm(14),
                    Size.Cm(2)),
                Weight.Grams(709),
                new NonEmptyList<CategoryId>(new CategoryId(1)));
    }
}