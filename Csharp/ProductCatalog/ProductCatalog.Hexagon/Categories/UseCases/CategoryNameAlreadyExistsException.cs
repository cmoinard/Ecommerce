using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Categories.UseCases
{
    public class CategoryNameAlreadyExistsException : DomainException
    {
        public CategoryNameAlreadyExistsException(CategoryName categoryName)
        {
            CategoryName = categoryName;
        }

        public CategoryName CategoryName { get; }
    }
}