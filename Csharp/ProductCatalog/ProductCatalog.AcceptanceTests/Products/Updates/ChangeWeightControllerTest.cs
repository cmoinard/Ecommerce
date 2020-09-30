using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products;
using Shared.Domain;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Products.Updates
{
    public class ChangeWeightControllerTest : TestBase
    {
        private static readonly Weight NewWeight = Weight.Grams(800);

        [Fact]
        public async Task ShouldThrowNotFound_WhenNoProduct()
        {
            var controller =
                BuildController(
                    RepositoryThatCantFindProduct());

            var actual =
                await controller.ChangeWeightAsync(
                    Guid.NewGuid(),
                    new ChangeWeightController.Dto
                    {
                        Weight = 800
                    });

            Check.That(actual).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenWeightIsInvalid()
        {
            var controller =
                BuildController(
                    RepositoryReturning(ProductSamples.TypeMatrix()));

            var actual =
                await controller.ChangeWeightAsync(
                    Guid.NewGuid(),
                    new ChangeWeightController.Dto
                    {
                        Weight = -10
                    });

            Check.That(actual).IsInstanceOf<BadRequestResult>();
        }

        [Fact]
        public async Task ShouldChangeWeight_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var saveProduct = Substitute.For<ISaveProduct>();
            var controller =
                BuildController(
                    RepositoryReturning(product),
                    saveProduct);

            var actual =
                await controller.ChangeWeightAsync(
                    Guid.NewGuid(),
                    new ChangeWeightController.Dto
                    {
                        Weight = 800
                    });

            Check.That(actual).IsInstanceOf<OkResult>();
            await saveProduct.Received().SaveAsync(product);
            Check.That(product.Weight).Equals(NewWeight);
        }

        private ChangeWeightController BuildController(
            IProductsRepository repository,
            ISaveProduct? saveProduct = null)
        {
            return new ChangeWeightController(
                new ChangeWeightUseCase(
                    repository,
                    saveProduct ?? Substitute.For<ISaveProduct>()));
        }
    }
}