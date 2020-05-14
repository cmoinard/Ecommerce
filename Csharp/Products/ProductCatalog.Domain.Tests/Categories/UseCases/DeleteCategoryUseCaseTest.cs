using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.Ports;
using ProductCatalog.Domain.Categories.UseCases;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Domain.Tests.Categories.UseCases
{
    public class DeleteCategoryUseCaseTest
    {
        private readonly CategoryId _id = new CategoryId(1);

        [Fact]
        public void ShouldThrowNotFoundException_WhenNoCategoryWithIdToDelete()
        {
            var useCase = 
                new DeleteCategoryUseCase(
                    RepositoryReturning(null),
                    Substitute.For<IProductCatalogUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.DeleteAsync(_id))
                .ThrowsNotFound(_id);
        }
        
        [Fact]
        public async Task ShouldDelete_WhenCategoryExists()
        {
            var category = new Category(_id, new CategoryName("Keyboards"));
            var repository = RepositoryReturning(category);
            var unitOfWork = Substitute.For<IProductCatalogUnitOfWork>();
            var useCase = new DeleteCategoryUseCase(repository, unitOfWork);

            await useCase.DeleteAsync(_id);
            
            Received.InOrder(async () =>
            {
                await repository.DeleteAsync(category);
                await unitOfWork.SaveChangesAsync();
            });
        }

        private ICategoriesRepository RepositoryReturning(Category? category)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.GetByIdAsync(_id).Returns(category);
            return repository;
        }
    }
}