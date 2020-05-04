using System.Collections.Generic;
using System.Linq;
using ProductCatalog.ApplicationServices.Products.UnvalidatedStates;
using ProductCatalog.Domain.CategoryAggregate;

namespace ProductCatalog.Web.Products.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DimensionDto Dimension { get; set; }

        public int Weight { get; set; }

        public IReadOnlyCollection<int> CategoryIds { get; set; }

        public UnvalidatedProduct ToDomain() =>
            new UnvalidatedProduct(
                Name,
                Description,
                Dimension.ToDomain(),
                Weight,
                CategoryIds
                    .Select(id => new CategoryId(id))
                    .ToList());
    }
}