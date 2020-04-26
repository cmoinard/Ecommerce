using System;
using System.Collections.Generic;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Size : ValueObject
    {
        private readonly uint _millimeters;

        private Size(uint millimeters)
        {
            if (millimeters < 1)
                throw new ArgumentOutOfRangeException("Size should be strictly positive");
            
            _millimeters = millimeters;
        }

        public static Size Cm(uint size) => new Size(size * 10);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _millimeters;
        }
    }
}