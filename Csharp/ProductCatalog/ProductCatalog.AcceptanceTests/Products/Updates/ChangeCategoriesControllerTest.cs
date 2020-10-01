using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products;
using Shared.Core;
using Shared.Domain;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Products.Updates
{
    public class ChangeCategoriesControllerTest : TestBase
    {
        [Fact]
        public async Task ShouldReturnNotFoundRequest_WhenNoProduct()
        {
            var controller =
                BuildController(
                    RepositoryThatCantFindProduct());

            var actual =
                await controller.ChangeCategoriesAsync(
                    Guid.NewGuid(),
                    new ChangeCategoriesController.Dto
                    {
                        CategoryIds = new List<int>{ 1 }
                    });

            Check.That(actual).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task ShouldReturnBadRequest_WhenNonExistentCategories()
        {
            var nonexistentId = new CategoryId(2222);
            var product = ProductSamples.TypeMatrix();
            
            var controller =
                BuildController(
                    RepositoryReturning(product),
                    CategoriesRepositoryWithNonExistentCategories(nonexistentId));

            var actual =
                await controller.ChangeCategoriesAsync(
                    (Guid) product.Id,
                    new ChangeCategoriesController.Dto
                    {
                        CategoryIds = new[] {1, 2222}
                    });

            Check.That(actual).IsInstanceOf<BadRequestResult>();
        }

        [Fact]
        public async Task ShouldChangeCategories_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var saveProduct = Substitute.For<ISaveProduct>();
            var controller =
                BuildController(
                    RepositoryReturning(product),
                    saveProduct: saveProduct);
            
            var actual =
                await controller.ChangeCategoriesAsync(
                    (Guid) product.Id,
                    new ChangeCategoriesController.Dto
                    {
                        CategoryIds = new[] { 2, 3 }
                    });

            Check.That(actual).IsInstanceOf<OkResult>();
            await saveProduct.Received().SaveAsync(product);
        }

        private ChangeCategoriesController BuildController(
            IProductsRepository repository,
            ICategoriesRepository? categoriesRepository = null,
            ISaveProduct? saveProduct = null)
        {
            return new ChangeCategoriesController(
                new ChangeCategoriesUseCase(
                    repository,
                    categoriesRepository ?? CategoriesRepositoryWithNonExistentCategories(),
                    saveProduct ?? Substitute.For<ISaveProduct>()));
        }

        private ICategoriesRepository CategoriesRepositoryWithNonExistentCategories(
            params CategoryId[] nonExistentCategoryIds)
        {
            var categoriesRepository = Substitute.For<ICategoriesRepository>();
            categoriesRepository
                .GetNonExistentIdsAsync(Arg.Any<NonEmptyList<CategoryId>>())
                .Returns(nonExistentCategoryIds);
            return categoriesRepository;
        }
    }
}