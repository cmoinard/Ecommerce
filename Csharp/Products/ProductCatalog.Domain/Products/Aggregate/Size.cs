using System.Collections.Generic;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace ProductCatalog.Domain.Products.Aggregate
{
    public class Size : SimpleValueObject<int>
    {
        private readonly int _millimeters;

        private Size(int millimeters)
            : base(millimeters)
        {
            Validate(millimeters).EnsureIsValid();
            _millimeters = millimeters;
        }

        public int ToCm() => _millimeters / 10;

        public static Size Cm(int size) => new Size(size * 10);

        public static Validation<Size> TryCm(int cm) =>
            Validate(cm * 10)
                .ToValidation(() => Cm(cm));
        
        private static IReadOnlyCollection<ValidationError> Validate(int size)
        {
            var errors = new List<ValidationError>();
            
            if (size < 1)
                errors.Add(new NegativeSizeValidationError());
            
            return errors;
        }
        
        public class NegativeSizeValidationError : SimpleValidationError {}
    }
}