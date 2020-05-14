using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;
using ProductCatalog.Domain.Products.UseCases;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Domain.Tests.Products.UseCases.Updates
{
    public class ChangeNameUseCaseTest : TestBase
    {
        private static readonly ProductName NewName = new ProductName("Typematrix 2030 DVORAK");
        
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeNameUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeNameAsync(id, NewName))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public void ShouldThrowException_WhenNameAlreadyExists()
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeNameUseCase(
                    RepositoryNameExistsReturning(true, product),
                    Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeNameAsync(product.Id, NewName))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, NewName);
        }
     
        [Fact]
        public async Task ShouldChangeName_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IProductCatalogUnitOfWork>();
            var useCase = 
                new ChangeNameUseCase(
                    RepositoryNameExistsReturning(false, product),
                    unitOfWork);
            
            await useCase.ChangeNameAsync(product.Id, NewName);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.Name).Equals(NewName);
        }

        private IProductsRepository RepositoryNameExistsReturning(bool nameExists, Product product)
        {
            var repository = Substitute.For<IProductsRepository>();
            repository
                .GetByIdAsync(Arg.Any<ProductId>())
                .Returns(product);
            repository
                .NameExistsAsync(Arg.Any<ProductName>(), Arg.Any<ProductId>())
                .Returns(nameExists);
            return repository;
        }
    }
}