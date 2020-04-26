using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using Products.ApplicationServices.Categories;
using Products.ApplicationServices.Categories.UseCases;
using Products.Domain.CategoryAggregate;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Xunit;

namespace Products.ApplicationServices.Tests.Categories.UseCases
{
    public class DeleteCategoryUseCaseTest
    {
        private readonly Category.Id _id = new Category.Id(1);

        [Fact]
        public void ShouldThrowNotFoundException_WhenNoCategoryWithIdToDelete()
        {
            var useCase = 
                new DeleteCategoryUseCase(
                    RepositoryReturning(null),
                    Substitute.For<IUnitOfWork>());
            
            Check
                .ThatAsyncCode(() => useCase.DeleteAsync(_id))
                .Throws<NotFoundException<Category.Id>>()
                .WithProperty(e => e.Id, _id);
        }
        
        [Fact]
        public async Task ShouldDelete_WhenCategoryExists()
        {
            var category = new Category(_id, "Keyboards".ToNonEmpty());
            var repository = RepositoryReturning(category);
            var unitOfWork = Substitute.For<IUnitOfWork>();
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