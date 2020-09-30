using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products;
using ProductCatalog.Web.Products.Dtos;
using Shared.Domain;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Products
{
    public class CreateProductControllerTest
    {
        private readonly ProductId _createdProductId = new ProductId(Guid.NewGuid());
        
        [Fact]
        public async Task ShouldReturnBadRequest_WhenNameAlreadyExists()
        {
            var controller =
                BuildController(
                    RepositoryWithNameExistsReturning(true));

            var actual = await controller.CreateAsync(UncreatedProduct());

            Check.That(actual).IsInstanceOf<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var repository = RepositoryWithNameExistsReturning(false);
            var createProduct = BuildCreateProduct();
            var controller = BuildController(repository, createProduct);

            var actual = await controller.CreateAsync(UncreatedProduct());

            Check.That(actual).IsInstanceOf<OkResult>();

            await createProduct.Received().CreateAsync(Arg.Any<UncreatedProduct>());
        }

        private ICreateProduct BuildCreateProduct()
        {
            var createProduct = Substitute.For<ICreateProduct>();
            createProduct.CreateAsync(Arg.Any<UncreatedProduct>()).Returns(_createdProductId);
            return createProduct;
        }

        private static CreateProductController BuildController(
            IProductsRepository repository,
            ICreateProduct? createProduct = null)
        {
            return new CreateProductController(
                new CreateProductUseCase(
                    repository,
                    createProduct ?? Substitute.For<ICreateProduct>()));
        }

        private static ProductDto UncreatedProduct() =>
            new ProductDto
            {
                Name = "Typematrix 2030 BÉPO",
                Description = "Best keyboard of the universe",
                Dimension = new DimensionDto
                {
                    Length = 33,
                    Width = 14,
                    Height = 2,
                },
                Weight = 709,
                CategoryIds = new[] {1}
            };

        private static IProductsRepository RepositoryWithNameExistsReturning(bool exists)
        {
            var repository = Substitute.For<IProductsRepository>();
            repository.NameExistsAsync(Arg.Any<ProductName>()).Returns(exists);
            return repository;
        }
    }
}