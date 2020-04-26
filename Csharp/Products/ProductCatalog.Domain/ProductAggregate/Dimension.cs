using System.Collections.Generic;
using Shared.Core.DomainModeling;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Dimension : ValueObject
    {
        public Dimension(Size length, Size width, Size height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public Size Length { get; }
        public Size Width { get; }
        public Size Height { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Length;
            yield return Width;
            yield return Height;
        }
    }
}