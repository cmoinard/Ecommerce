using NFluent;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Validations;
using Xunit;

namespace ProductCatalog.Domain.Tests.ProductAggregate
{
    public class ProductDescriptionTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        public void ShouldBeInvalid_WhenEmpty(string name)
        {
            var actual = ProductDescription.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<ProductDescription>(
                        new ProductDescription.EmptyValidationError()));
        }

        [Fact]
        public void ShouldBeInvalid_WhenExceedsMaxLength()
        {
            var name = new string('e', ProductDescription.MaxLength + 10);
            
            var actual = ProductDescription.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Invalid<ProductDescription>(
                        new ProductDescription.ExceedsMaxLengthValidationError()));
        }

        [Theory]
        [InlineData(@"êàyeTvþ")]
        [InlineData(@"1234567890")]
        public void ShouldBeValid_WhenNameIsValid(string name)
        {
            var actual = ProductDescription.TryCreate(name);

            Check.That(actual)
                .IsEqualTo(
                    Validation.Valid(new ProductDescription(name)));
        }
    }
}