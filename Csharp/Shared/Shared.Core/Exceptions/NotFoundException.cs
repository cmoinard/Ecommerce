namespace Shared.Core.Exceptions
{
    public class NotFoundException<T> : DomainException
    {
        public NotFoundException(T id)
        {
            Id = id;
        }

        public T Id { get; }
    }
}