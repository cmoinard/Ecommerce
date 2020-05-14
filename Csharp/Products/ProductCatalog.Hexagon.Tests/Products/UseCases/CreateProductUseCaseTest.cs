using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Products;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using Shared.Core;
using Shared.Domain;
using Xunit;

namespace ProductCatalog.Hexagon.Tests.Products.UseCases
{
    public class CreateProductUseCaseTest
    {
        [Fact]
        public void ShouldThrowException_WhenNameAlreadyExists()
        {
            var product = UncreatedProduct();
            var useCase = new CreateProductUseCase(
                RepositoryWithNameExistsReturning(true),
                Substitute.For<ICreateProduct>());

            Check
                .ThatAsyncCode(() => useCase.CreateAsync(product))
                .Throws<ProductNameAlreadyExistsException>()
                .WithProperty(e => e.Name, product.Name);
        }
     
        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var repository = RepositoryWithNameExistsReturning(false);
            var createProduct = Substitute.For<ICreateProduct>();
            var useCase = new CreateProductUseCase(repository, createProduct);

            await useCase.CreateAsync(UncreatedProduct());

            await createProduct.Received().CreateAsync(Arg.Any<UncreatedProduct>());
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