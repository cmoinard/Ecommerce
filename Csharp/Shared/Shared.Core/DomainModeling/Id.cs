namespace Shared.Core.DomainModeling
{
    public abstract class Id<T> : SimpleValueObject<T>
        where T : notnull
    {
        private readonly T _value;

        protected Id(T value)
            : base(value)
        {
            _value = value;
        }

        public static explicit operator T(Id<T> valueObject) => 
            valueObject._value;
    }
}