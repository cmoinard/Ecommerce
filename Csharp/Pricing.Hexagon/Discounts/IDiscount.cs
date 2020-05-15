using System.Collections.Generic;
using Pricing.Hexagon.Products.Aggregate;

namespace Pricing.Hexagon.Discounts
{
    public interface IDiscount
    {
        Price ApplyOn(Price price);
    }

    public static class DiscountExtensions
    {
        public static Price ApplyOn(this IEnumerable<IDiscount> source, Price price)
        {
            var p = price;
            
            foreach (var discount in source) 
                discount.ApplyOn(p);

            return p;
        }
    }
}