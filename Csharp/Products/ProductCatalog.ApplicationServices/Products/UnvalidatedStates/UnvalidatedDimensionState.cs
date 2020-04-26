using System.Collections.Generic;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Validations;

namespace ProductCatalog.ApplicationServices.Products.UnvalidatedStates
{
    public class UnvalidatedDimensionState  : IValidatable
    {
        public UnvalidatedDimensionState(int length, int width, int height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public int Length { get; }
        public int Width { get; }
        public int Height { get; }

        public IEnumerable<ValidationError> Validate()
        {
            if (Length < 1)
                yield return new NegativeLengthValidationError();
            if (Width < 1)
                yield return new NegativeWidthValidationError();
            if (Height < 1)
                yield return new NegativeHeightValidationError();
        }

        public Dimension ToDomain() =>
            new Dimension(
                Size.Cm((uint) Length),
                Size.Cm((uint) Width),
                Size.Cm((uint) Height)
            );
    }

    public class NegativeLengthValidationError : SimpleValidationError
    {
    }

    public class NegativeWidthValidationError : SimpleValidationError
    {
    }

    public class NegativeHeightValidationError : SimpleValidationError
    {
    }
}