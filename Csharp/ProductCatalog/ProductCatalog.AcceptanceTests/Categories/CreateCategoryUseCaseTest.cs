using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Web.Categories;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Categories
{
    public class CreateCategoryUseCaseTest
    {
        [Fact]
        public async Task ShouldReturn400_WhenCategoryNameAlreadyExists()
        {
            var repository = BuildRepository(nameExists: true);
            var controller = BuildController(repository);

            var actual =
                await controller.PostAsync(
                    new CreateCategoryDto{ CategoryName = "claviers"});

            Check.That(actual).IsInstanceOf<BadRequestObjectResult>();
            Check.That(((BadRequestObjectResult) actual).Value)
                .Equals("Name claviers already exists");
        }

        [Fact]
        public async Task ShouldReturn400_WhenCategoryNameIsInvalid()
        {
            var controller =
                BuildController(
                    BuildRepository(nameExists: false));

            var actual =
                await controller.PostAsync(
                    new CreateCategoryDto{ CategoryName = "     "});

            Check.That(actual).IsInstanceOf<BadRequestObjectResult>();
        }

        [Fact]
        public async Task ShouldReturn201_WhenCategoryNameIsValid()
        {
            var controller = 
                BuildController(
                    BuildRepository(nameExists: true));

            var actual =
                await controller.PostAsync(
                    new CreateCategoryDto{ CategoryName = "écrans"});

            Check.That(actual).IsInstanceOf<CreatedResult>();
        }

        private static ICategoriesRepository BuildRepository(bool nameExists)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.NameAlreadyExistsAsync(Arg.Any<string>()).Returns(nameExists);
            return repository;
        }

        private static CategoriesController BuildController(ICategoriesRepository repository) =>
            new CategoriesController(
                Substitute.For<IGetCategoriesUseCase>(),
                Substitute.For<IDeleteCategoryUseCase>(),
                new CreateCategoryUseCase(repository));
    }
}