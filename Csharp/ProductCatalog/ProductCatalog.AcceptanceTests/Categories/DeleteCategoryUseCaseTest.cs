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
                BuildController(
                    Substitute.For<ICategoriesRepository>());

            var actual = await useCase.DeleteAsync("format invalide");
            
            Check.That(actual).IsInstanceOf<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ShouldReturn200_WhenDeletingInexistantCategory()
        {
            var repository = BuildRepository(false);
            var useCase = BuildController(repository);

            var actual = await useCase.DeleteAsync("AC7CFC06-E3AD-44C1-836D-8C3AD2C579F3");

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.DidNotReceive().DeleteAsync(Arg.Any<CategoryId>());
        }

        [Fact]
        public async Task ShouldReturn200_WhenDeletingExistantCategory()
        {
            var repository = BuildRepository(true);
            var useCase = BuildController(repository);

            var id = "AC7CFC06-E3AD-44C1-836D-8C3AD2C579F3";
            var existingId = new CategoryId(new Guid(id));
            var actual = await useCase.DeleteAsync(id);

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.Received().DeleteAsync(existingId);
        }

        private static ICategoriesRepository BuildRepository(bool exists)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.ExistsAsync(Arg.Any<CategoryId>()).Returns(exists);
            return repository;
        }

        private static CategoriesController BuildController(ICategoriesRepository categoriesRepository) =>
            new CategoriesController(
                Substitute.For<IGetCategoriesUseCase>(),
                new DeleteCategoryUseCase(categoriesRepository),
                Substitute.For<ICreateCategoryUseCase>());
    }
}