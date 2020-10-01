using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Products.Updates
{
    public class ChangeNameControllerTest : TestBase
    {
        private const string NewName = "Typematrix 2030 DVORAK";

        [Fact]
        public async Task ShouldReturnNotFound_WhenNoProduct()
        {
            var controller =
                BuildController(
                    RepositoryThatCantFindProduct());

            var actual =
                await controller.ChangeNameAsync(
                    Guid.NewGuid(),
                    new ChangeNameController.Dto
                    {
                        Name = "Toto"
                    });

            Check.That(actual).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenInvalidName()
        {
            var controller =
                BuildController(
                    RepositoryReturning(ProductSamples.TypeMatrix()));

            var actual =
                await controller.ChangeNameAsync(
                    Guid.NewGuid(),
                    new ChangeNameController.Dto
                    {
                        Name = "    "
                    });

            Check.That(actual).IsInstanceOf<BadRequestResult>();
        }

        [Fact]
        public async Task ShouldReturnOk_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<ISaveProduct>();
            var controller =
                BuildController(
                    RepositoryReturning(product),
                    unitOfWork);

            var actual =
                await controller.ChangeNameAsync(
                    Guid.NewGuid(),
                    new ChangeNameController.Dto
                    {
                        Name = NewName
                    });

            Check.That(actual).IsInstanceOf<OkResult>();
            await unitOfWork.Received().SaveAsync(product);
            Check.That(product.Name).Equals(new ProductName(NewName));
        }

        private ChangeNameController BuildController(
            IProductsRepository repository,
            ISaveProduct? saveProduct = null)
        {
            return new ChangeNameController(
                new ChangeNameUseCase(
                    repository,
                    saveProduct ?? Substitute.For<ISaveProduct>()));
        }
    }
}