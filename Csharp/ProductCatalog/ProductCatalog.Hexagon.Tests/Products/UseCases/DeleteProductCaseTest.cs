using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using Shared.Domain;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Hexagon.Tests.Products.UseCases
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
                    Substitute.For<IDeleteProduct>());

            Check
                .ThatAsyncCode(() => useCase.DeleteAsync(id))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public async Task ShouldDelete_WhenProductIsFound()
        {
            var product = ProductSamples.TypeMatrix();
            var repository = RepositoryReturning(product);
            var deleteProduct = Substitute.For<IDeleteProduct>();
            var useCase = new DeleteProductUseCase(repository, deleteProduct);
            
            await useCase.DeleteAsync(product.Id);
            
            await deleteProduct.Received().DeleteAsync(product);
        }
    }
}