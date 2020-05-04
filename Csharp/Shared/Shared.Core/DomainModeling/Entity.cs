using System.Collections.Generic;

namespace Shared.Core.DomainModeling
{
    public abstract class Entity<TId>
        where TId : notnull
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; }

        public override bool Equals(object? obj) =>
            obj is Entity<TId> other &&
            EqualityComparer<TId>.Default.Equals(Id, other.Id);

        public override int GetHashCode() => 
            EqualityComparer<TId>.Default.GetHashCode(Id);
    }
}