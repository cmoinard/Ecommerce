using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Web.Categories;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Categories
{
    public class DeleteCategoryUseCaseTest
    {
        [Fact]
        public async Task ShouldReturn400_WhenDeletingInvalidCategoryId()
        {
            var useCase =
                new CategoriesController(
                    Substitute.For<IGetCategoriesUseCase>(),
                    new DeleteCategoryUseCase(
                        Substitute.For<ICategoriesRepository>()));

            var actual = await useCase.DeleteAsync("format invalide");

            Check.That(actual).IsInstanceOf<BadRequestObjectResult>();
        }
        
        [Fact]
        public async Task ShouldReturn200_WhenDeletingInexistantCategory()
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.ExistsAsync(Arg.Any<CategoryId>()).Returns(false);
            
            var useCase =
                new CategoriesController(
                    Substitute.For<IGetCategoriesUseCase>(),
                    new DeleteCategoryUseCase(
                        repository));

            var actual = await useCase.DeleteAsync("AC7CFC06-E3AD-44C1-836D-8C3AD2C579F3");

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.DidNotReceive().DeleteAsync(Arg.Any<CategoryId>());
        }
        
        [Fact]
        public async Task ShouldReturn200_WhenDeletingExistantCategory()
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.ExistsAsync(Arg.Any<CategoryId>()).Returns(true);
            
            var useCase =
                new CategoriesController(
                    Substitute.For<IGetCategoriesUseCase>(),
                    new DeleteCategoryUseCase(
                        repository));

            var id = "AC7CFC06-E3AD-44C1-836D-8C3AD2C579F3";
            var existingId = new CategoryId(new Guid(id));
            var actual = await useCase.DeleteAsync(id);

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.Received().DeleteAsync(existingId);
        }
    }
}