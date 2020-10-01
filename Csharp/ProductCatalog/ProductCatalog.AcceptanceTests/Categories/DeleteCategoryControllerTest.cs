using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using ProductCatalog.Web.Categories;
using Shared.Domain;
using Xunit;

namespace ProductCatalog.AcceptanceTests.Categories
{
    public class DeleteCategoryControllerTest
    {
        [Fact]
        public async Task ShouldReturn200_WhenDeletingInexistantCategory()
        {
            var repository = BuildRepository(false);
            var useCase = BuildController(repository);

            var actual = await useCase.DeleteAsync(2222);

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.DidNotReceive().DeleteAsync(Arg.Any<CategoryId>());
        }

        [Fact]
        public async Task ShouldReturn200_WhenDeletingExistantCategory()
        {
            var repository = BuildRepository(true);
            var useCase = BuildController(repository);

            var categoryId = new CategoryId(1);
            var actual = await useCase.DeleteAsync(1);

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.Received().DeleteAsync(categoryId);
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