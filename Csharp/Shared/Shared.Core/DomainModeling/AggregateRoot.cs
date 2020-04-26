namespace Shared.Core.DomainModeling
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        protected AggregateRoot()
        {
        }
        
        protected AggregateRoot(TId id)
            : base(id)
        {
        }
    }
}