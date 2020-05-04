using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Weight : SimpleValueObject<int>
    {
        private readonly int _grams;

        private Weight(int grams)
            : base(grams)
        {
            if (grams < 1)
                throw new ArgumentOutOfRangeException("Weight should be strictly positive");
            _grams = grams;
        }

        public int ToGrams() => _grams;

        public static Weight Grams(int grams) => new Weight(grams);
        public static Weight Kg(int kg) => new Weight(kg * 1000);
    }
}