using NFluent;
using ProductCatalog.Hexagon.Categories.Aggregate;
using Shared.Core.Validations;
using Xunit;

namespace ProductCatalog.UnitTests.Categories
{
    public class CategoryNameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void Validation_ShouldBeInvalid_WhenNameIsEmpty(string name)
        {
            Check.That(CategoryName.TryCreate(name))
                .Equals(
                    Validation.Invalid<CategoryName>(
                        new CategoryName.EmptyValidationError()));
        }
        
        [Fact]
        public void Validation_ShouldBeInvalid_WhenNameIsTooLong()
        {
            var name = new string('c', 200);
            Check.That(CategoryName.TryCreate(name))
                .Equals(
                    Validation.Invalid<CategoryName>(
                        new CategoryName.ExceedsMaxLengthValidationError()));
        }
        
        [Fact]
        public void Validation_ShouldBeValid_WhenNameIsOk()
        {
            Check.That(CategoryName.TryCreate("claviers"))
                .Equals(
                    Validation.Valid(new CategoryName("claviers")));
        }
    }
}