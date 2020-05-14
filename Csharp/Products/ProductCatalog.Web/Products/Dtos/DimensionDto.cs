using System.Collections.Generic;
using ProductCatalog.Domain.Products.Aggregate;
using Shared.Core;
using Shared.Core.Validations;

namespace ProductCatalog.Web.Products.Dtos
{
    public class DimensionDto
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public Validation<Dimension> Validate()
        {
            var l = Size.TryCm(Length);
            var w = Size.TryCm(Width);
            var h = Size.TryCm(Height);

            return Validate(l, w, h)
                .ToValidation(() => new Dimension(l.Value, w.Value, h.Value));
        }

        private static IReadOnlyCollection<ValidationError> Validate(
            Validation<Size> length, 
            Validation<Size> width,
            Validation<Size> height)
        {
            var errors = new List<ValidationError>();
            
            if (length.IsInvalid)
                errors.Add(new LengthValidationError(length.Errors));
            
            if (width.IsInvalid)
                errors.Add(new WidthValidationError(width.Errors));

            if (height.IsInvalid)
                errors.Add(new HeightValidationError(height.Errors));

            return errors;
        }

        public abstract class SizeValidationError : ValidationError
        {
            protected SizeValidationError(NonEmptyList<ValidationError> sizeErrors)
            {
                SizeErrors = sizeErrors;
            }

            public NonEmptyList<ValidationError> SizeErrors { get; }

            protected override IEnumerable<object> GetEqualityComponents() => 
                SizeErrors;
        }
        
        public class HeightValidationError : SizeValidationError
        {
            public HeightValidationError(NonEmptyList<ValidationError> sizeErrors)
                : base(sizeErrors)
            {
            }
        }
        
        public class WidthValidationError : SizeValidationError
        {
            public WidthValidationError(NonEmptyList<ValidationError> sizeErrors) 
                : base(sizeErrors)
            {
            }
        }
        
        public class LengthValidationError : SizeValidationError
        {
            public LengthValidationError(NonEmptyList<ValidationError> sizeErrors) 
                : base(sizeErrors)
            {
            }
        }
    }
}