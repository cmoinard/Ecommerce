using System;
using System.Collections.Generic;
using NFluent;
using Pricing.Hexagon.Discounts;
using Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies;
using Shared.Domain;
using Xunit;

namespace Pricing.UnitTests
{
    public class ProductGlobalWeightDiscountStrategyTest
    {
        private static ProductId ProductId1 { get; } = new ProductId(Guid.NewGuid());
        private static ProductId ProductId2 { get; } = new ProductId(Guid.NewGuid());
        private static ProductId ProductId3 { get; } = new ProductId(Guid.NewGuid());
        private static ProductId ProductId4 { get; } = new ProductId(Guid.NewGuid());
        private static ProductId ProductId5 { get; } = new ProductId(Guid.NewGuid());
        private static ProductId ProductId6 { get; } = new ProductId(Guid.NewGuid());
        private static ProductId ProductId7 { get; } = new ProductId(Guid.NewGuid());
        
        private readonly IReadOnlyDictionary<ProductId, Weight> _weightByProductId =
            new Dictionary<ProductId, Weight>
            {
                [ProductId1] = Weight.Grams(10),
                [ProductId2] = Weight.Grams(500),
                [ProductId3] = Weight.Kg(10),
                [ProductId4] = Weight.Kg(20),
                [ProductId5] = Weight.Kg(49.99m),
                [ProductId6] = Weight.Kg(50),
                [ProductId7] = Weight.Kg(80)
            };

        private readonly IProductGlobalDiscountStrategy _strategy;

        public ProductGlobalWeightDiscountStrategyTest()
        {
            _strategy = new ProductGlobalWeightDiscountStrategy(_weightByProductId);
        }
        
        public static TheoryData<IReadOnlyCollection<ProductId>> NoDiscountProductIds =
            new TheoryData<IReadOnlyCollection<ProductId>>
            {
                new []{ ProductId1 },
                new []{ ProductId2 },
                new []{ ProductId5 },
                new []{ ProductId3, ProductId4 },
            };
        
        [Theory]
        [MemberData(nameof(NoDiscountProductIds))]
        public void ShouldHaveNoDiscount_WhenWeightIsBelowThreshold(IReadOnlyCollection<ProductId> productIds)
        {
            Check.That(_strategy.GetDiscount(productIds))
                .Equals(Discount.None);
        }
        
        public static TheoryData<IReadOnlyCollection<ProductId>> WithDiscountProductIds =
            new TheoryData<IReadOnlyCollection<ProductId>>
            {
                new []{ ProductId6 },
                new []{ ProductId7 },
                new []{ ProductId4, ProductId4, ProductId4 },
            };
        
        [Theory]
        [MemberData(nameof(WithDiscountProductIds))]
        public void ShouldHave5PercentDiscount_WhenWeightIsHigherThanThreshold(IReadOnlyCollection<ProductId> productIds)
        {
            Check.That(_strategy.GetDiscount(productIds))
                .Equals(Discount.Percent(5));
        }
    }
}