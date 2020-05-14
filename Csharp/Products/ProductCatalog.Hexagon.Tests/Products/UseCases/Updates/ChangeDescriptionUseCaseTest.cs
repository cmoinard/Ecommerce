using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Hexagon.Tests.Products.UseCases.Updates
{
    public class ChangeDescriptionUseCaseTest : TestBase
    {
        private static readonly ProductDescription NewDescription = new ProductDescription("Best keyboard of the world");
        
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeDescriptionUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<ISaveProduct>());

            Check
                .ThatAsyncCode(() => useCase.ChangeDescriptionAsync(id, NewDescription))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public async Task ShouldChangeDescription_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<ISaveProduct>();
            var useCase = 
                new ChangeDescriptionUseCase(
                    RepositoryReturning(product),
                    unitOfWork);
            
            await useCase.ChangeDescriptionAsync(product.Id, NewDescription);

            await unitOfWork.Received().SaveAsync(product);
            Check.That(product.Description).Equals(NewDescription);
        }
    }
}