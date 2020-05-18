using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using Shared.Domain;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Hexagon.Tests.Products.UseCases.Updates
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
                    Substitute.For<ISaveProduct>());

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
                    Substitute.For<ISaveProduct>());

            Check
                .ThatAsyncCode(() => useCase.ChangeNameAsync(product.Id, NewName))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, NewName);
        }
     
        [Fact]
        public async Task ShouldChangeName_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var saveProduct = Substitute.For<ISaveProduct>();
            var useCase = 
                new ChangeNameUseCase(
                    RepositoryNameExistsReturning(false, product),
                    saveProduct);
            
            await useCase.ChangeNameAsync(product.Id, NewName);

            await saveProduct.Received().SaveAsync(product);
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