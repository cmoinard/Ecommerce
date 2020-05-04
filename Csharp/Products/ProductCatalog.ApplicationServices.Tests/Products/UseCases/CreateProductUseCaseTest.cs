using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.Extensions;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases
{
    public class CreateProductUseCaseTest
    {
        [Fact]
        public void ShouldThrowValidationException_WhenStateIsInvalid()
        {
            var state = UnvalidatedProductWithCategories();
            var useCase = new CreateProductUseCase(
                RepositoryWithNameExistsReturning(false),
                Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.CreateAsync(state))
                .ThrowsValidationException(new EmptyCategoriesValidationError());
        }
        
        [Fact]
        public void ShouldThrowException_WhenNameAlreadyExists()
        {
            var state = UnvalidatedProductWithCategories(new CategoryId(1));
            var useCase = new CreateProductUseCase(
                RepositoryWithNameExistsReturning(true),
                Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.CreateAsync(state))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, state.Name.ToNonEmpty());
        }
     
        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var repository = RepositoryWithNameExistsReturning(false);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = new CreateProductUseCase(repository, unitOfWork);

            await useCase.CreateAsync(
                UnvalidatedProductWithCategories(new CategoryId(1)));

            Received.InOrder(async () =>
            {
                await repository.Received().CreateAsync(Arg.Any<UncreatedProduct>());
                await unitOfWork.Received().SaveChangesAsync();
            });
        }

        private static IProductsRepository RepositoryWithNameExistsReturning(bool exists)
        {
            var repository = Substitute.For<IProductsRepository>();
            repository.NameExistsAsync(Arg.Any<NonEmptyString>()).Returns(exists);
            return repository;
        }

        private static UnvalidatedProduct UnvalidatedProductWithCategories(params CategoryId[] categoryIds) =>
            new UnvalidatedProduct(
                "Typematrix 2030 BÉPO",
                "Best keyboard of the universe",
                new UnvalidatedDimension(33, 14, 2), 
                709,
                categoryIds);
    }
}