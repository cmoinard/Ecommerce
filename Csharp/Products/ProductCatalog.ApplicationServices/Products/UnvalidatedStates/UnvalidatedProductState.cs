using System.Collections.Generic;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.ApplicationServices.Products.UnvalidatedStates
{
    public class UnvalidatedProductState : IValidatable
    {
        public UnvalidatedProductState(
            string name,
            string description,
            UnvalidatedDimensionState dimensionState,
            int weightInGrams,
            IReadOnlyCollection<CategoryId> categoryIds)
        {
            Name = name;
            Description = description;
            DimensionState = dimensionState;
            WeightInGrams = weightInGrams;
            CategoryIds = categoryIds;
        }

        public string Name { get; }
        public string Description { get; }
        public UnvalidatedDimensionState DimensionState { get; }
        public int WeightInGrams { get; }
        public IReadOnlyCollection<CategoryId> CategoryIds { get; }

        public IEnumerable<ValidationError> Validate()
        {
            if (Name.IsNullOrWhiteSpace())
                yield return new EmptyProductNameValidationError();
            
            if (Description.IsNullOrWhiteSpace())
                yield return new EmptyProductDescriptionValidationError();

            foreach (var validationError in DimensionState.Validate())
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
                DimensionState.ToDomain(),
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