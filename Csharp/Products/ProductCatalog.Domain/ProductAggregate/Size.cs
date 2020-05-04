using System;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Size : SimpleValueObject<int>
    {
        private readonly int _millimeters;

        private Size(int millimeters)
            : base(millimeters)
        {
            if (millimeters < 1)
                throw new ArgumentOutOfRangeException("Size should be strictly positive");
            _millimeters = millimeters;
        }

        public int ToCm() => _millimeters / 10;

        public static Size Cm(int size) => new Size(size * 10);
    }
}