using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using ProductCatalog.ApplicationServices.Categories;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
using Shared.Core.Extensions;
using Shared.Testing;
using Xunit;

namespace ProductCatalog.ApplicationServices.Tests.Products.UseCases.Updates
{
    public class ChangeCategoriesUseCaseTest : TestBase
    {
        [Fact]
        public void ShouldThrowNotFound_WhenNoProduct()
        {
            var id = new ProductId(Guid.NewGuid());
            var useCase = 
                new ChangeCategoriesUseCase(
                    RepositoryThatCantFindProduct(),
                    CategoriesRepositoryWithNonExistentCategories(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeCategoriesAsync(id, new[] {new CategoryId(2)}))
                .ThrowsNotFound(id);
        }
        
        [Fact]
        public void ShouldThrowValidationException_WhenNoCategories()
        {
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeCategoriesUseCase(
                    RepositoryReturning(product),
                    CategoriesRepositoryWithNonExistentCategories(),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeCategoriesAsync(product.Id, new List<CategoryId>()))
                .ThrowsValidationException(new EmptyCategoriesValidationError());
        }
        
        [Fact]
        public void ShouldThrowException_WhenNonExistentCategories()
        {
            var nonexistentId = new CategoryId(2);
            var product = ProductSamples.TypeMatrix();
            var useCase = 
                new ChangeCategoriesUseCase(
                    RepositoryReturning(product),
                    CategoriesRepositoryWithNonExistentCategories(nonexistentId),
                    Substitute.For<IUnitOfWork>());

            Check
                .ThatAsyncCode(() => useCase.ChangeCategoriesAsync(product.Id, new[]{ nonexistentId }))
                .Throws<NonExistentCategoriesException>()
                .WithProperty(e => e.Ids, new NonEmptyList<CategoryId>(nonexistentId));
        }
        
        [Fact]
        public async Task ShouldChangeCategories_WhenAllIsValid()
        {
            var product = ProductSamples.TypeMatrix();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var useCase = 
                new ChangeCategoriesUseCase(
                    RepositoryReturning(product),
                    CategoriesRepositoryWithNonExistentCategories(),
                    unitOfWork);

            var newCategories = new[] {new CategoryId(2), new CategoryId(3)};
            await useCase.ChangeCategoriesAsync(product.Id, newCategories);

            await unitOfWork.Received().SaveChangesAsync();
            Check.That(product.CategoryIds).Equals(newCategories.ToNonEmptyList());
        }

        private ICategoriesRepository CategoriesRepositoryWithNonExistentCategories(
            params CategoryId[] nonExistentCategoryIds)
        {
            var categoriesRepository = Substitute.For<ICategoriesRepository>();
            categoriesRepository
                .GetNonExistentIdsAsync(Arg.Any<NonEmptyList<CategoryId>>())
                .Returns(nonExistentCategoryIds);
            return categoriesRepository;
        }
    }
}