using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Products;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Domain.Products.Ports;
using ProductCatalog.Domain.Products.UseCases;
using Shared.Core;
using Xunit;

namespace ProductCatalog.Domain.Tests.Products.UseCases
{
    public class CreateProductUseCaseTest
    {
        [Fact]
        public void ShouldThrowException_WhenNameAlreadyExists()
        {
            var product = UncreatedProduct();
            var useCase = new CreateProductUseCase(
                RepositoryWithNameExistsReturning(true),
                Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.CreateAsync(product))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, product.Name);
        }
     
        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var repository = RepositoryWithNameExistsReturning(false);
            var unitOfWork = Substitute.For<IProductCatalogUnitOfWork>();
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