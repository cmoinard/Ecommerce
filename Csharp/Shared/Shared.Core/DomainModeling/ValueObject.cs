using System.Collections.Generic;

namespace Shared.Core.DomainModeling
{
    public abstract class ValueObject : StructuralEqualityObject
    {
    }

    public abstract class SimpleValueObject<T> : ValueObject
        where T : notnull
    {
        private readonly T _value;

        public SimpleValueObject(T value)
        {
            _value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }

        public override string ToString() => 
            _value.ToString()!;

        public static explicit operator T(SimpleValueObject<T> valueObject) => 
            valueObject._value;
    }
}