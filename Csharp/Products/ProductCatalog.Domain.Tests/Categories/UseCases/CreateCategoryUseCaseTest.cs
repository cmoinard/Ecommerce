using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Domain.Categories;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.Ports;
using ProductCatalog.Domain.Categories.UseCases;
using Xunit;

namespace ProductCatalog.Domain.Tests.Categories.UseCases
{
    public class CreateCategoryUseCaseTest
    {
        [Fact]
        public void ShouldThrowDomainException_WhenNameAlreadyExists()
        {
            var name = new CategoryName("Keyboards");
            var useCase = 
                new CreateCategoryUseCase(
                    RepositoryWithExistsReturning(true),
                    Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => 
                    useCase.CreateAsync(new UncreatedCategory(name)))
                .Throws<CategoryNameAlreadyExistsException>()
                .WithProperty(e => e.Name, name);
        }
        
        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var name = new CategoryName("Keyboards");
            var repository = RepositoryWithExistsReturning(false);
            var unitOfWork = Substitute.For<IProductCatalogUnitOfWork>();
            var useCase = new CreateCategoryUseCase(repository, unitOfWork);

            await useCase.CreateAsync(new UncreatedCategory(name));

            Received.InOrder(async () =>
            {
                await repository.Received().CreateAsync(Arg.Any<UncreatedCategory>());
                await unitOfWork.Received().SaveChangesAsync();
            });
        }

        private static ICategoriesRepository RepositoryWithExistsReturning(bool exists)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.NameExistsAsync(Arg.Any<CategoryName>()).Returns(exists);
            return repository;
        }
    }
}