using System;
using System.Collections.Generic;
using Shared.Core.DomainModeling;

namespace Pricing.Hexagon.Sales
{
    public class Period : ValueObject
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }

        public bool Contains(DateTime date) =>
            Start <= date && date < End;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }

        public static Period Unlimited(DateTime start) =>
            new Period(start, DateTime.MaxValue);
    }
}