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
    public class DeleteCategoryUseCaseTest
    {
        [Fact]
        public async Task ShouldReturn200_WhenDeletingInexistantCategory()
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.ExistsAsync(Arg.Any<string>()).Returns(false);
            
            var useCase =
                new CategoriesController(
                    Substitute.For<IGetCategoriesUseCase>(),
                    new DeleteCategoryUseCase(
                        repository));

            var actual = await useCase.DeleteAsync("j'existe pas");

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.DidNotReceive().DeleteAsync(Arg.Any<string>());
        }
        
        [Fact]
        public async Task ShouldReturn200_WhenDeletingExistantCategory()
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.ExistsAsync(Arg.Any<string>()).Returns(true);
            
            var useCase =
                new CategoriesController(
                    Substitute.For<IGetCategoriesUseCase>(),
                    new DeleteCategoryUseCase(
                        repository));

            var actual = await useCase.DeleteAsync("claviers");

            Check.That(actual).IsInstanceOf<OkResult>();
            await repository.Received().DeleteAsync("claviers");
        }
    }
}