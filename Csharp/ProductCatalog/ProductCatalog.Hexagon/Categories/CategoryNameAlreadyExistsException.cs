using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Categories
{
    public class CategoryNameAlreadyExistsException : DomainException
    {
        public CategoryNameAlreadyExistsException(string categoryName)
        {
            CategoryName = categoryName;
        }

        public string CategoryName { get; }
    }
}