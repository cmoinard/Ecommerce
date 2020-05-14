using NFluent;
using ProductCatalog.Domain.Categories.Aggregate;
using Shared.Core.Validations;
using Xunit;

namespace ProductCatalog.Domain.Tests.Categories.Aggregate
{
    public class CategoryNameTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        public void ShouldBeInvalid_WhenEmpty(string name)
        {
            var actual = CategoryName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<CategoryName>(
                        new CategoryName.EmptyValidationError()));
        }

        [Fact]
        public void ShouldBeInvalid_WhenExceedsMaxLength()
        {
            var name = new string('e', CategoryName.MaxLength + 10);
            
            var actual = CategoryName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<CategoryName>(
                        new CategoryName.ExceedsMaxLengthValidationError()));
        }

        [Theory]
        [InlineData(@"Ee@")]
        [InlineData(@"Ee\")]
        public void ShouldBeInvalid_WhenHasInvalidCharacters(string name)
        {
            var actual = CategoryName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<CategoryName>(
                        new CategoryName.InvalidCharactersValidationError()));
        }

        [Theory]
        [InlineData(@"êàyeTvþ")]
        [InlineData(@"1234567890")]
        public void ShouldBeValid_WhenNameIsValid(string name)
        {
            var actual = CategoryName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Valid(new CategoryName(name)));
        }
    }
}