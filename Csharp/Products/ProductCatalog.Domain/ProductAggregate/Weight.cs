using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Weight : SimpleValueObject<int>
    {
        private Weight(int grams)
            : base(grams)
        {
            if (grams < 1)
                throw new ArgumentOutOfRangeException("Weight should be strictly positive");
        }

        public static Weight Grams(int grams) => new Weight(grams);
        public static Weight Kg(int kg) => new Weight(kg * 1000);
    }
}