using System.Collections.Generic;

namespace Shared.Core.DomainModeling
{
    public abstract class ValueObject : StructuralEqualityObject
    {
    }

    public abstract class SimpleValueObject<T> : ValueObject
        where T : notnull
    {
        protected SimpleValueObject(T value)
        {
            InternalValue = value;
        }

        protected T InternalValue { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InternalValue;
        }

        public override string ToString() => 
            InternalValue.ToString()!;
    }

    public abstract class StringBasedValueObject : SimpleValueObject<string>
    {
        protected StringBasedValueObject(string internalValue)
            : base(internalValue)
        {
        }
        
        public static explicit operator string(StringBasedValueObject valueObject) => 
            valueObject.InternalValue;
    }
}