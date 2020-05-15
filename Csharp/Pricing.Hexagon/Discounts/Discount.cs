using Pricing.Hexagon.Products.Aggregate;

namespace Pricing.Hexagon.Discounts
{
    public static class Discount
    {
        public static IDiscount None { get; } =
            new NoDiscount();
        
        public static IDiscount Percent(int percent) =>
            new DiscountPercent(percent);
        
        public static IDiscount Price(decimal price) =>
            new DiscountPrice(price);

        private class NoDiscount : IDiscount
        {
            public Price ApplyOn(Price price) => price;
        }
    }
}