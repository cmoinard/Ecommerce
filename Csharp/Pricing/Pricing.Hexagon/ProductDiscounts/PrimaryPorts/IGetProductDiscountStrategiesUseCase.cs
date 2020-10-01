using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.Aggregate;
using Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies;
using Shared.Core;
using Shared.Domain;

using DiscountStrategiesByProductId = 
    System.Collections.Generic.IReadOnlyDictionary<
        Shared.Domain.ProductId,
        System.Collections.Generic.IReadOnlyCollection<
            Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies.IProductGlobalDiscountStrategy>>;

namespace Pricing.Hexagon.ProductDiscounts.PrimaryPorts
{
    public interface IGetProductDiscountStrategiesUseCase
    {
        Task<IReadOnlyCollection<DiscountStrategy>> GetGlobalDiscountStrategiesAsync(NonEmptyList<ProductId> productIds);
    }
}