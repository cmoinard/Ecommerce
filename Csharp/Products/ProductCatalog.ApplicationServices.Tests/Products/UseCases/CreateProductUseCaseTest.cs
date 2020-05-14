using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Products;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases
{
    public class CreateProductUseCaseTest
    {
        [Fact]
        public void ShouldThrowException_WhenNameAlreadyExists()
        {
            var product = UncreatedProduct();
            var useCase = new CreateProductUseCase(
                RepositoryWithNameExistsReturning(true),
                Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.CreateAsync(product))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, product.Name);
        }
     
        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var repository = RepositoryWithNameExistsReturning(false);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = new CreateProductUseCase(repository, unitOfWork);

            await useCase.CreateAsync(UncreatedProduct());

            Received.InOrder(async () =>
            {
                await repository.Received().CreateAsync(Arg.Any<UncreatedProduct>());
                await unitOfWork.Received().SaveChangesAsync();
            });
        }

        private static IProductsRepository RepositoryWithNameExistsReturning(bool exists)
        {
            var repository = Substitute.For<IProductsRepository>();
            repository.NameExistsAsync(Arg.Any<ProductName>()).Returns(exists);
            return repository;
        }

        private static UncreatedProduct UncreatedProduct() =>
            new UncreatedProduct(
                new ProductName("Typematrix 2030 BÉPO"),
                new ProductDescription("Best keyboard of the universe"),
                new Dimension(
                    Size.Cm(33), 
                    Size.Cm(14),
                    Size.Cm(2)), 
                Weight.Grams(709), 
                new NonEmptyList<CategoryId>(new CategoryId(1)));
    }
}