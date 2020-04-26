using System;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
using Shared.Core.Extensions;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases.Updates
{
    public class ChangeNameUseCaseTest : TestBase
    {
        private const string NewName = "Typematrix 2030 DVORAK";
        
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeNameProductUseCase(
                    RepositoryThatCantFindProduct(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeNameAsync(id, NewName))
                .ThrowsNotFound(id);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void ShouldThrowValidationException_WhenNameIsEmpty(string newName)
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeNameProductUseCase(
                    RepositoryNameExistsReturning(false, product),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeNameAsync(product.Id, newName))
                .ThrowsValidationException(new EmptyProductNameValidationError());
        }
        
        [Fact]
        public void ShouldThrowException_WhenNameAlreadyExists()
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeNameProductUseCase(
                    RepositoryNameExistsReturning(true, product),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeNameAsync(product.Id, NewName))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, NewName.ToNonEmpty());
        }
     
        [Fact]
        public async Task ShouldChangeName_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = 
                new ChangeNameProductUseCase(
                    RepositoryNameExistsReturning(false, product),
                    unitOfWork);
            
            await useCase.ChangeNameAsync(product.Id, NewName);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.Name).Equals(NewName.ToNonEmpty());
        }

        private IProductsRepository RepositoryNameExistsReturning(bool nameExists, Product product)
        {
            var repository = Substitute.For<IProductsRepository>();
            repository
                .GetByIdAsync(Arg.Any<ProductId>())
                .Returns(product);
            repository
                .NameExistsAsync(Arg.Any<NonEmptyString>(), Arg.Any<ProductId>())
                .Returns(nameExists);
            return repository;
        }
    }
}