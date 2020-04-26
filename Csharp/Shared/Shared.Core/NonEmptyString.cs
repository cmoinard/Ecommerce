using System;
using System.Collections.Generic;

namespace Shared.Core
{
    public class NonEmptyString : StructuralEqualityObject
    {
        private readonly string _value;

        public NonEmptyString(string value)
        {
            var trimmedValue = value.Trim();
            if (string.IsNullOrWhiteSpace(trimmedValue))
                throw new ArgumentException("Cannot be empty");
            
            _value = trimmedValue;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
        
        public static explicit operator string(NonEmptyString nonEmptyString) => 
            nonEmptyString._value;
    }
}