using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.SecondaryPorts
{
    public interface IProductPriceRepository
    {
        Task<IReadOnlyDictionary<ProductId, Price>> GetPriceByProductIdAsync(NonEmptyList<ProductId> productIds);
    }
}