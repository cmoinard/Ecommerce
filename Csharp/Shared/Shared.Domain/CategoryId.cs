using Shared.Core.DomainModeling;

namespace Shared.Domain
{
    public class CategoryId : Id<int>
    {
        public CategoryId(int internalValue) : base(internalValue)
        {
        }
    }
}