using System.Collections.Generic;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.ApplicationServices.Products.UnvalidatedStates
{
    public class UnvalidatedProduct : IValidatable
    {
        public UnvalidatedProduct(
            string name,
            string description,
            UnvalidatedDimension dimension,
            int weightInGrams,
            IReadOnlyCollection<CategoryId> categoryIds)
        {
            Name = name;
            Description = description;
            Dimension = dimension;
            WeightInGrams = weightInGrams;
            CategoryIds = categoryIds;
        }

        public string Name { get; }
        public string Description { get; }
        public UnvalidatedDimension Dimension { get; }
        public int WeightInGrams { get; }
        public IReadOnlyCollection<CategoryId> CategoryIds { get; }

        public IEnumerable<ValidationError> Validate()
        {
            if (Name.IsNullOrWhiteSpace())
                yield return new EmptyProductNameValidationError();
            
            if (Description.IsNullOrWhiteSpace())
                yield return new EmptyProductDescriptionValidationError();

            foreach (var validationError in Dimension.Validate())
                yield return validationError;

            if (WeightInGrams < 0)
                yield return new NegativeWeightValidationError();

            if (CategoryIds.None())
                yield return new EmptyCategoriesValidationError();
        }
        
        public Product ToDomain() =>
            new Product(
                Name.ToNonEmpty(),
                Description.ToNonEmpty(),
                Dimension.ToDomain(),
                Weight.Grams(WeightInGrams),
                CategoryIds.ToNonEmptyList());
    }
    
    public class EmptyProductNameValidationError : SimpleValidationError
    {
    }
    
    public class EmptyProductDescriptionValidationError : SimpleValidationError
    {
    }

    public class NegativeWeightValidationError : SimpleValidationError
    {
    }

    public class EmptyCategoriesValidationError : SimpleValidationError
    {
    }
}