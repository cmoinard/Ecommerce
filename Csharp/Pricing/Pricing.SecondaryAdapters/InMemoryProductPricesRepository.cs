using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Pricing.Hexagon.Products.SecondaryPorts;
using Shared.Core;
using Shared.Domain;

namespace Pricing.SecondaryAdapters
{
    public class InMemoryProductPricesRepository : IProductPricesRepository
    {
        public Task<ProductPrice?> GetByIdAsync(ProductId id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<ProductPrice>> GetAsync(NonEmptyList<ProductId> productIds)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync(ProductPrice productPrice)
        {
            throw new System.NotImplementedException();
        }
    }
}