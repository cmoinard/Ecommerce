using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.Products.SecondaryPorts
{
    public interface IProductPricesRepository
    {
        Task<ProductPrice?> GetByIdAsync(ProductId id);
        Task<IReadOnlyCollection<ProductPrice>> GetAsync(NonEmptyList<ProductId> productIds);
        Task SaveAsync(ProductPrice productPrice);
    }
}