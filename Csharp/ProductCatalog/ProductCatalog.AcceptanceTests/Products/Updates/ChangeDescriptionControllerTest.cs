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
    public class ChangeDescriptionControllerTest : TestBase
    {
        private const string NewDescription = "Best keyboard of the world";
        
        [Fact]
        public async Task ShouldReturnNotFound_WhenNoProduct()
        {
            var controller =
                BuildController(
                    RepositoryThatCantFindProduct());

            var actual =
                await controller.ChangeDescriptionAsync(
                    Guid.NewGuid(),
                    new ChangeDescriptionController.Dto
                    {
                        Description = "Toto"
                    });

            Check.That(actual).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenInvalidDescription()
        {
            var controller =
                BuildController(
                    RepositoryReturning(ProductSamples.TypeMatrix()));

            var actual =
                await controller.ChangeDescriptionAsync(
                    Guid.NewGuid(),
                    new ChangeDescriptionController.Dto
                    {
                        Description = "    "
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
                await controller.ChangeDescriptionAsync(
                    Guid.NewGuid(),
                    new ChangeDescriptionController.Dto
                    {
                        Description = NewDescription
                    });

            Check.That(actual).IsInstanceOf<OkResult>();
            await unitOfWork.Received().SaveAsync(product);
            Check.That(product.Description).Equals(new ProductDescription(NewDescription));
        }

        private ChangeDescriptionController BuildController(
            IProductsRepository repository,
            ISaveProduct? saveProduct = null)
        {
            return new ChangeDescriptionController(
                new ChangeDescriptionUseCase(
                    repository,
                    saveProduct ?? Substitute.For<ISaveProduct>()));
        }
    }
}