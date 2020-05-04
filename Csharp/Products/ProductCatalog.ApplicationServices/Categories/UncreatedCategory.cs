using Shared.Core;

namespace ProductCatalog.ApplicationServices.Categories
{
    public class UncreatedCategory
    {
        public UncreatedCategory(NonEmptyString name)
        {
            Name = name;
        }

        public NonEmptyString Name { get; }
    }
}