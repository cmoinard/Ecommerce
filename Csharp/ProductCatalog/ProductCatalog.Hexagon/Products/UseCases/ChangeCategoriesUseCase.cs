using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.SecondaryPorts;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.SecondaryPorts;
using Shared.Core;
using Shared.Core.Exceptions;

namespace ProductCatalog.Hexagon.Products.UseCases
{
    public class ChangeCategoriesUseCase : ProductUseCaseBase, IChangeCategoriesUseCase
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ISaveProduct _saveProduct;

        public ChangeCategoriesUseCase(
            IProductsRepository repository,
            ICategoriesRepository categoriesRepository,
            ISaveProduct saveProduct)
            : base(repository)
        {
            _categoriesRepository = categoriesRepository;
            _saveProduct = saveProduct;
        }

        public async Task ChangeCategoriesAsync(ProductId productId, NonEmptyList<CategoryId> categoryIds)
        {
            var product = await SafeGetProductAsync(productId);
            
            var nonexistentCategoryIds = await _categoriesRepository.GetNonExistentIdsAsync(categoryIds);
            if (nonexistentCategoryIds.Any())
                throw new NonExistentCategoriesException(categoryIds);

            product.CategoryIds = categoryIds;

            await _saveProduct.SaveAsync(product);
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