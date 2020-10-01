using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Core.DomainModeling;
using Shared.Core.Validations;

namespace Shared.Domain
{
    public class Weight : ComparableValueObject<int>
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
        public static Weight Kg(decimal kg) => new Weight((int)(kg * 1000));
        
        public static Weight operator +(Weight w1, Weight w2) =>
            new Weight(w1.InternalValue + w2.InternalValue);

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

    public static class WeightExtensions
    {
        public static Weight Sum<T>(this IEnumerable<T> source, Func<T, Weight> getWeight)
        {
            var totalGrams =
                source
                    .Select(s => getWeight(s).ToGrams())
                    .Sum();
            
            return Weight.Grams(totalGrams);
        }
    }
}