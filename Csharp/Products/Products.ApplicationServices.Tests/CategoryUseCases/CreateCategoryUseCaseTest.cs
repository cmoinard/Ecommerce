using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using Products.ApplicationServices.Categories;
using Products.ApplicationServices.Categories.UseCases;
using Products.Domain.CategoryAggregate;
using Shared.Core;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;
using Xunit;

namespace Products.ApplicationServices.Tests.CategoryUseCases
{
    public class CreateCategoryUseCaseTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void ShouldThrowValidationException_WhenNameIsEmpty(string name)
        {
            var useCase = 
                new CreateCategoryUseCase(
                    Substitute.For<ICategoriesRepository>(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => 
                    useCase.CreateAsync(new UnvalidatedCategoryState(name)))
                .Throws<ValidationException>()
                .WithProperty(
                    e => e.Errors, 
                    new NonEmptyList<ValidationError>(new EmptyCategoryNameValidationError()));
        }
        
        [Fact]
        public void ShouldThrowDomainException_WhenNameAlreadyExists()
        {
            var useCase = 
                new CreateCategoryUseCase(
                    RepositoryWithExistsReturning(true),
                    Substitute.For<IUnitOfWork>());
            
            Check
                .ThatAsyncCode(() => 
                    useCase.CreateAsync(new UnvalidatedCategoryState("Keyboards")))
                .Throws<CategoryNameAlreadyExistsException>()
                .WithProperty(e => e.Name, "Keyboards".ToNonEmpty());
        }
        
        [Fact]
        public async Task ShouldCreate_WhenAllIsValid()
        {
            var repository = RepositoryWithExistsReturning(false);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = new CreateCategoryUseCase(repository, unitOfWork);

            await useCase.CreateAsync(new UnvalidatedCategoryState("Keyboards"));

            Received.InOrder(async () =>
            {
                await repository.Received().CreateAsync(Arg.Any<Category>());
                await unitOfWork.Received().SaveChangesAsync();
            });
        }

        private static ICategoriesRepository RepositoryWithExistsReturning(bool exists)
        {
            var repository = Substitute.For<ICategoriesRepository>();
            repository.NameExistsAsync(Arg.Any<NonEmptyString>()).Returns(exists);
            return repository;
        }
    }
}