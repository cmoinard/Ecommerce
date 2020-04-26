using System;
using System.Collections.Generic;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Weight : ValueObject
    {
        private readonly uint _grams;

        private Weight(uint grams)
        {
            if (grams < 1)
                throw new ArgumentOutOfRangeException("Weight should be strictly positive");

            _grams = grams;
        }

        public static Weight Grams(uint grams) => new Weight(grams);
        public static Weight Kg(uint kg) => new Weight(kg * 1000);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _grams;
        }
    }
}