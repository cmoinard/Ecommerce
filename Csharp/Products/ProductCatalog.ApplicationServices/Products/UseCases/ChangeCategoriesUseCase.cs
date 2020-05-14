using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.ApplicationServices.Categories;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core;
using Shared.Core.Exceptions;

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

        public async Task ChangeCategoriesAsync(ProductId productId, NonEmptyList<CategoryId> categoryIds)
        {
            var product = await SafeGetProductAsync(productId);
            
            var nonexistentCategoryIds = await _categoriesRepository.GetNonExistentIdsAsync(categoryIds);
            if (nonexistentCategoryIds.Any())
                throw new NonExistentCategoriesException(categoryIds);

            product.CategoryIds = categoryIds;

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