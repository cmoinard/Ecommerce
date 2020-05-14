using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.UseCases;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Domain.Tests.Products.UseCases
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
                    Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.DeleteAsync(id))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public async Task ShouldDelete_WhenProductIsFound()
        {
            var product = ProductSamples.TypeMatrix();
            var repository = RepositoryReturning(product);
            var unitOfWork = Substitute.For<IProductCatalogUnitOfWork>();
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