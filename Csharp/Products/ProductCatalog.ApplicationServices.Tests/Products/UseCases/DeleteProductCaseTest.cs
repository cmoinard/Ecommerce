using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases
{
    public class DeleteProductCaseTest : TestBase
    {
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new DeleteProductUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.DeleteAsync(id))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public async Task ShouldDelete_WhenProductIsFound()
        {
            var product = ProductSamples.TypeMatrix();
            var repository = RepositoryReturning(product);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = new DeleteProductUseCase(repository, unitOfWork);
            
            await useCase.DeleteAsync(product.Id);
            
            Received.InOrder(async () =>
            {
                await repository.Received().DeleteAsync(product);
                await unitOfWork.Received().SaveChangesAsync();
            });
        }
    }
}