using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.UseCases;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Domain.Tests.Products.UseCases.Updates
{
    public class ChangeDimensionUseCaseTest : TestBase
    {
        private static readonly Dimension NewDimension = 
            new Dimension(
                Size.Cm(20), 
                Size.Cm(30), 
                Size.Cm(40));
        
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeDimensionUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeDimensionsAsync(id, NewDimension))
                .ThrowsNotFound(id);
        }
     
        [Fact]
        public async Task ShouldChangeDimension_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IProductCatalogUnitOfWork>();
            var useCase = 
                new ChangeDimensionUseCase(
                    RepositoryReturning(product),
                    unitOfWork);
            
            await useCase.ChangeDimensionsAsync(product.Id, NewDimension);

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