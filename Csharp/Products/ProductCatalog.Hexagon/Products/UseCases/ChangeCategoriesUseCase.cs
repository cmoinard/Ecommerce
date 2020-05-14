using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.Ports;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.Ports;
using Shared.Core;
using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeCategoriesUseCase : ProductUseCaseBase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IProductCatalogUnitOfWork _productCatalogUnitOfWork;

        public ChangeCategoriesUseCase(
            IProductsRepository repository,
            ICategoriesRepository categoriesRepository,
            IProductCatalogUnitOfWork productCatalogUnitOfWork)
            : base(repository)
        {
            _categoriesRepository = categoriesRepository;
            _productCatalogUnitOfWork = productCatalogUnitOfWork;
        }

        public async Task ChangeCategoriesAsync(ProductId productId, NonEmptyList<CategoryId> categoryIds)
        {
            var product = await SafeGetProductAsync(productId);
            
            var nonexistentCategoryIds = await _categoriesRepository.GetNonExistentIdsAsync(categoryIds);
            if (nonexistentCategoryIds.Any())
                throw new NonExistentCategoriesException(categoryIds);

            product.CategoryIds = categoryIds;

            await _productCatalogUnitOfWork.SaveChangesAsync();
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