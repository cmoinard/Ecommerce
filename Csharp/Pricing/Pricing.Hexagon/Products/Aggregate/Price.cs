using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace Pricing.Hexagon.Products.Aggregate
{
    public class Price : ComparableValueObject<decimal>
    {
        public Price(decimal value) 
            : base(value)
        {
            Validate(value).EnsureIsValid();
        }

        private static IReadOnlyCollection<ValidationError> Validate(decimal price)
        {
            var errors = new List<ValidationError>();
            if (price < 0)
                errors.Add(new NegativePriceValidationError());
            
            if (price % 0.01m != 0)
                errors.Add(new BelowCentValidationError());

            return errors;
        }

        public static Price operator *(Price p, decimal v) =>
            new Price(p.InternalValue * v);
        
        public static Price operator +(Price p, decimal v) =>
            new Price(p.InternalValue + v);
        
        public static Price operator -(Price p, decimal v) =>
            new Price(p.InternalValue - v);
        
        public static Price operator +(Price p1, Price p2) =>
            new Price(p1.InternalValue + p2.InternalValue);

        public class NegativePriceValidationError : SimpleValidationError{}

        public class BelowCentValidationError : SimpleValidationError{}
    }

    public static class PriceExtensions
    {
        public static Price Sum<T>(this IEnumerable<T> source, Func<T, Price> getPrice) =>
            source
                .Select(s => getPrice(s))
                .Sum();
        
        public static Price Sum(this IEnumerable<Price> source) =>
            source.Aggregate((acc, p) => acc + p);
    }
}