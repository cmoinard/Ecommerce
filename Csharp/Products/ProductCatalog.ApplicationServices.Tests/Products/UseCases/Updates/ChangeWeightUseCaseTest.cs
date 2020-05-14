using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases.Updates
{
    public class ChangeWeightUseCaseTest : TestBase
    {
        private static readonly Weight NewWeight = Weight.Grams(800);

        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeWeightUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeWeightAsync(id, NewWeight))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public async Task ShouldChangeWeight_WhenAllIsValid()
        {
            var weight = NewWeight;
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = 
                new ChangeWeightUseCase(
                    RepositoryReturning(product),
                    unitOfWork);
            
            await useCase.ChangeWeightAsync(product.Id, weight);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.Weight).Equals(NewWeight);
        }
    }
}