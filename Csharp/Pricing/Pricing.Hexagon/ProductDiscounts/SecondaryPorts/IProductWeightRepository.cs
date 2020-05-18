using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.SecondaryPorts
{
    public interface IProductWeightRepository
    {
        Task<IReadOnlyDictionary<ProductId, Weight>> GetWeightByProductIdAsync(NonEmptyList<ProductId> productIds);
    }
}