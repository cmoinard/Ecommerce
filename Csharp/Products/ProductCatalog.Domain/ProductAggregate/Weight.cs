using System.Collections.Generic;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace ProductCatalog.Domain.ProductAggregate
{
    public class Weight : SimpleValueObject<int>
    {
        private readonly int _grams;

        private Weight(int grams)
            : base(grams)
        {
            ValidateGrams(grams).EnsureIsValid();
            
            _grams = grams;
        }

        public int ToGrams() => _grams;

        public static Weight Grams(int grams) => new Weight(grams);
        public static Weight Kg(int kg) => new Weight(kg * 1000);

        public static Validation<Weight> TryGrams(int grams) =>
            ValidateGrams(grams)
                .ToValidation(() => Grams(grams));

        private static IReadOnlyCollection<ValidationError> ValidateGrams(int grams)
        {
            var errors = new List<ValidationError>();
            
            if (grams < 1)
                errors.Add(new NegativeSizeValidationError());
            
            return errors;
        }
        
        public class NegativeSizeValidationError : SimpleValidationError {}
    }
}