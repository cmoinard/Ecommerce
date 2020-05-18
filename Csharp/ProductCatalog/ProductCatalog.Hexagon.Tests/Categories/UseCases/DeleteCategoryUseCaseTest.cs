using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using Shared.Domain;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.Hexagon.Tests.Categories.UseCases
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
                    Substitute.For<IDeleteCategory>());

            Check
                .ThatAsyncCode(() => useCase.DeleteAsync(_id))
                .ThrowsNotFound(_id);
        }
        
        [Fact]
        public async Task ShouldDelete_WhenCategoryExists()
        {
            var category = new Category(_id, new CategoryName("Keyboards"));
            var repository = RepositoryReturning(category);
            var deleteCategory = Substitute.For<IDeleteCategory>();
            var useCase = new DeleteCategoryUseCase(repository, deleteCategory);

            await useCase.DeleteAsync(_id);
            
            await deleteCategory.Received().DeleteAsync(category);
        }

        private ICategoriesRepository RepositoryReturning(Category? category)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.GetByIdAsync(_id).Returns(category);
            return repository;
        }
    }
}