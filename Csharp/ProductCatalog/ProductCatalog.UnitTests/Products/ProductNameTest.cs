using NFluent;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core.Validations;
using Xunit;

namespace ProductCatalog.UnitTests.Products
{
    public class ProductNameTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        public void ShouldBeInvalid_WhenEmpty(string name)
        {
            var actual = ProductName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<ProductName>(
                        new ProductName.EmptyValidationError()));
        }

        [Fact]
        public void ShouldBeInvalid_WhenExceedsMaxLength()
        {
            var name = new string('e', ProductName.MaxLength + 10);
            
            var actual = ProductName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<ProductName>(
                        new ProductName.ExceedsMaxLengthValidationError()));
        }

        [Theory]
        [InlineData(@"Ee@")]
        [InlineData(@"Ee\")]
        public void ShouldBeInvalid_WhenHasInvalidCharacters(string name)
        {
            var actual = ProductName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<ProductName>(
                        new ProductName.InvalidCharactersValidationError()));
        }

        [Theory]
        [InlineData(@"êàyeTvþ")]
        [InlineData(@"1234567890")]
        public void ShouldBeValid_WhenNameIsValid(string name)
        {
            var actual = ProductName.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Valid(new ProductName(name)));
        }
    }
}