namespace Shared.Core.DomainModeling
{
    public abstract class Id<T> : SimpleValueObject<T>
        where T : notnull
    {
        private readonly T _internalValue;

        protected Id(T internalValue)
            : base(internalValue)
        {
            _internalValue = internalValue;
        }

        public static explicit operator T(Id<T> valueObject) => 
            valueObject._internalValue;
    }
}