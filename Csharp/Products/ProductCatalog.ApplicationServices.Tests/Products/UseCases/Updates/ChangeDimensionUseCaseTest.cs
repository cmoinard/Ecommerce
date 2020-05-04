using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases.Updates
{
    public class ChangeDimensionUseCaseTest : TestBase
    {
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeDimensionUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IUnitOfWork>());

            var newDimension = new UnvalidatedDimension(20, 30, 40);
            Check
                .ThatAsyncCode(() => useCase.ChangeDimensionsAsync(id, newDimension))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public void ShouldThrowValidationException_WhenDimensionIsInvalid()
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeDimensionUseCase(
                    RepositoryReturning(product),
                    Substitute.For<IUnitOfWork>());

            var newDimension = new UnvalidatedDimension(-20, -30, -40);
            Check
                .ThatAsyncCode(() => useCase.ChangeDimensionsAsync(product.Id, newDimension))
                .ThrowsValidationException(
                    new NegativeLengthValidationError(),
                    new NegativeWidthValidationError(),
                    new NegativeHeightValidationError());
        }
     
        [Fact]
        public async Task ShouldChangeDimension_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = 
                new ChangeDimensionUseCase(
                    RepositoryReturning(product),
                    unitOfWork);
            
            var newDimension = new UnvalidatedDimension(20, 30, 40);
            await useCase.ChangeDimensionsAsync(product.Id, newDimension);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.Dimension)
                .Equals(
                    new Dimension(
                        Size.Cm(20),
                        Size.Cm(30),
                        Size.Cm(40)));
        }
    }
}