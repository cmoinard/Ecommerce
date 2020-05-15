using System.Collections.Generic;
using System.Linq;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Products;
using ProductCatalog.Hexagon.Products.Aggregate;
using Shared.Core;
using Shared.Core.Extensions;
using Shared.Core.Validations;
using Shared.Domain;

namespace ProductCatalog.Web.Products.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DimensionDto Dimension { get; set; }

        public int Weight { get; set; }

        public IReadOnlyCollection<int> CategoryIds { get; set; }

        public Validation<UncreatedProduct> Validate()
        {
            var name = ProductName.TryCreate(Name);
            var description = ProductDescription.TryCreate(Description);
            var dimension = Dimension.Validate();
            var weight = Shared.Domain.Weight.TryGrams(Weight);
            var categories = ValidateCategories(CategoryIds);

            var errors =
                name.SafeGetErrors()
                    .Concat(description.SafeGetErrors())
                    .Concat(dimension.SafeGetErrors())
                    .Concat(weight.SafeGetErrors())
                    .Concat(categories.SafeGetErrors())
                    .ToList();

            return errors.ToValidation(() => 
                new UncreatedProduct(
                    name.Value,
                    description.Value,
                    dimension.Value,
                    weight.Value,
                    categories.Value));
        }
        
        public static Validation<NonEmptyList<CategoryId>> ValidateCategories(IReadOnlyCollection<int> categoryIds)
        {
            var typedCategoryIds =
                categoryIds
                    .Select(id => new CategoryId(id))
                    .ToList();

            return
                typedCategoryIds.Any()
                    ? Validation.Valid(typedCategoryIds.ToNonEmptyList())
                    : Validation.Invalid<NonEmptyList<CategoryId>>(
                        new EmptyCategoriesValidationError());
        }
            
        public class EmptyCategoriesValidationError : SimpleValidationError {}
    }
}