using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Extensions;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases.Updates
{
    public class ChangeDescriptionUseCaseTest : TestBase
    {
        private const string NewDescription = "Best keyboard of the world";
        
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeDescriptionUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeDescriptionAsync(id, NewDescription))
                .ThrowsNotFound(id);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void ShouldThrowValidationException_WhenNameIsEmpty(string newDescription)
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeDescriptionUseCase(
                    RepositoryReturning(product),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeDescriptionAsync(product.Id, newDescription))
                .ThrowsValidationException(new EmptyProductDescriptionValidationError());
        }
        
        [Fact]
        public async Task ShouldChangeDescription_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = 
                new ChangeDescriptionUseCase(
                    RepositoryReturning(product),
                    unitOfWork);
            
            await useCase.ChangeDescriptionAsync(product.Id, NewDescription);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.Description).Equals(NewDescription.ToNonEmpty());
        }
    }
}