using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Categories;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace ProductCatalog.ApplicationServices.Products.UseCases
{
    public class ChangeCategoriesUseCase : ProductUseCaseBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeCategoriesUseCase(
            IProductsRepository repository,
            ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork)
            : base(repository)
        {
            _categoriesRepository = categoriesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ChangeCategoriesAsync(ProductId productId, IReadOnlyCollection<CategoryId> categoryIds)
        {
            var product = await SafeGetProductAsync(productId);
            
            if (categoryIds.None())
                throw new ValidationException(new EmptyCategoriesValidationError());
            
            var validatedCategoryIds = categoryIds.ToNonEmptyList();
            
            var nonexistentCategoryIds = await _categoriesRepository.GetNonExistentIdsAsync(validatedCategoryIds);
            if (nonexistentCategoryIds.Any())
                throw new NonExistentCategoriesException(validatedCategoryIds);

            product.CategoryIds = validatedCategoryIds;

            await _unitOfWork.SaveChangesAsync();
        }
    }
    
    public class NonExistentCategoriesException : DomainException
    {
        public NonExistentCategoriesException(NonEmptyList<CategoryId> ids)
        {
            Ids = ids;
        }

        public NonEmptyList<CategoryId> Ids { get; }
    }
}