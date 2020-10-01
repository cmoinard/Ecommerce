using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Products
{
    public class DeleteProductControllerTest : TestBase
    {
        [Fact]
        public async Task ShouldReturn200_WhenProductDoesntExist()
        {
            var controller =
                BuildController(
                    RepositoryThatCantFindProduct());

            var actual = await controller.DeleteAsync(Guid.NewGuid());

            Check.That(actual).IsInstanceOf<OkResult>();
        }

        [Fact]
        public async Task ShouldDelete_WhenProductIsFound()
        {
            var product = ProductSamples.TypeMatrix();
            var repository = RepositoryReturning(product);
            var deleteProduct = Substitute.For<IDeleteProduct>();
            var controller = BuildController(repository, deleteProduct);
            
            var actual = await controller.DeleteAsync((Guid)product.Id);
            
            await deleteProduct.Received().DeleteAsync(product);
            Check.That(actual).IsInstanceOf<OkResult>();
        }

        private DeleteProductController BuildController(
            IProductsRepository repository,
            IDeleteProduct? deleteProduct = null)
        {
            return new DeleteProductController(
                new DeleteProductUseCase(
                    repository,
                    deleteProduct ?? Substitute.For<IDeleteProduct>()));
        }
    }
}