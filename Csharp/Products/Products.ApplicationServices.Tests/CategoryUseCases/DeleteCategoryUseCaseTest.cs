using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using Products.ApplicationServices.CategoryUseCases;
using Products.ApplicationServices.CategoryUseCases.UseCases;
using Products.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.Exceptions;
using Xunit;

namespace Products.ApplicationServices.Tests.CategoryUseCases
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
            var category = new Category(_id, new NonEmptyString("Keyboards"));
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