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
    public class ChangeWeightUseCaseTest : TestBase
    {
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeWeightUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeWeightAsync(id, 800))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public void ShouldThrowValidationException_WhenWeightIsNegative()
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeWeightUseCase(
                    RepositoryReturning(product),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeWeightAsync(product.Id, -10))
                .ThrowsValidationException(new NegativeWeightValidationError());
        }
        
        [Fact]
        public async Task ShouldChangeWeight_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = 
                new ChangeWeightUseCase(
                    RepositoryReturning(product),
                    unitOfWork);
            
            await useCase.ChangeWeightAsync(product.Id, 800);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.Weight).Equals(Weight.Grams(800));
        }
    }
}