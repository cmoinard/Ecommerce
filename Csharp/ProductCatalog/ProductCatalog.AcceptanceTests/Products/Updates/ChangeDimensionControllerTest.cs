using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products;
using ProductCatalog.Web.Products.Dtos;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Products.Updates
{
    public class ChangeDimensionControllerTest : TestBase
    {
        [Fact]
        public async Task ShouldReturnNotFound_WhenNoProduct()
        {
            var controller =
                BuildController(
                    RepositoryThatCantFindProduct());

            var actual =
                await controller.ChangeDimensionAsync(
                    Guid.NewGuid(),
                    new DimensionDto
                    {
                        Height = 20,
                        Width = 30,
                        Length = 40
                    });

            Check.That(actual).IsInstanceOf<NotFoundResult>();
        }
        
        [Fact]
        public async Task ShouldReturnBadRequest_WhenInvalidDimension()
        {
            var controller =
                BuildController(
                    RepositoryReturning(ProductSamples.TypeMatrix()));

            var actual =
                await controller.ChangeDimensionAsync(
                    Guid.NewGuid(),
                    new DimensionDto
                    {
                        Height = -50,
                        Width = 30,
                        Length = 40
                    });

            Check.That(actual).IsInstanceOf<BadRequestResult>();
        }

        [Fact]
        public async Task ShouldReturnOk_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var saveProduct = Substitute.For<ISaveProduct>();
            var controller =
                BuildController(
                    RepositoryReturning(product),
                    saveProduct);

            var actual =
                await controller.ChangeDimensionAsync(
                    Guid.NewGuid(),
                    new DimensionDto
                    {
                        Length = 20,
                        Width = 30,
                        Height = 40,
                    });

            Check.That(actual).IsInstanceOf<OkResult>();
            await saveProduct.Received().SaveAsync(product);
            Check.That(product.Dimension)
                .Equals(
                    new Dimension(
                        Size.Cm(20),
                        Size.Cm(30),
                        Size.Cm(40)));
        }

        private ChangeDimensionController BuildController(
            IProductsRepository repository,
            ISaveProduct? saveProduct = null)
        {
            return new ChangeDimensionController(
                new ChangeDimensionUseCase(
                    repository,
                    saveProduct ?? Substitute.For<ISaveProduct>()));
        }
    }
}