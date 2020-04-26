using Shared.Core;
using Shared.Core.DomainModeling;

namespace Products.Domain.CategoryAggregate
{
    public class Category : AggregateRoot<Category.Id>
    {
        public Category(NonEmptyString name)
        {
            Name = name;
        }
        
        public Category(Id id, NonEmptyString name)
            : base(id)
        {
            Name = name;
        }

        public NonEmptyString Name { get; }

        public class Id : SimpleValueObject<int>
        {
            public Id(int value) 
                : base(value)
            {
            }
        }
    }
}