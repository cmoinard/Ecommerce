using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Web.Categories;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Categories
{
    public class CreateCategoryControllerTest
    {
        private readonly CategoryId _createdCategoryId;

        public CreateCategoryControllerTest()
        {
            _createdCategoryId = new CategoryId(1);
        }

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
        public async Task ShouldReturn200_WhenCategoryNameIsValid()
        {
            var controller = 
                BuildController(
                    BuildRepository(nameExists: false));

            var actual =
                await controller.PostAsync(
                    new CreateCategoryDto{ CategoryName = "écrans"});

            Check.That(actual).IsInstanceOf<OkObjectResult>();
            Check.That(((OkObjectResult) actual).Value)
                .Equals(_createdCategoryId);
        }

        private ICategoriesRepository BuildRepository(bool nameExists)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            
            repository.NameAlreadyExistsAsync(Arg.Any<CategoryName>()).Returns(nameExists);
            repository.CreateAsync(Arg.Any<UncreatedCategory>()).Returns(_createdCategoryId);
            
            return repository;
        }

        private static CategoriesController BuildController(ICategoriesRepository repository) =>
            new CategoriesController(
                Substitute.For<IGetCategoriesUseCase>(),
                Substitute.For<IDeleteCategoryUseCase>(),
                new CreateCategoryUseCase(repository));
    }
}