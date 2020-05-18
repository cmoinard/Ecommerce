using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.Strategies;
using Shared.Core;
using Shared.Domain;

using DiscountStrategiesByProductId = 
    System.Collections.Generic.IReadOnlyDictionary<
        Shared.Domain.ProductId,
        System.Collections.Generic.IReadOnlyCollection<
            Pricing.Hexagon.ProductDiscounts.Strategies.IProductGlobalDiscountStrategy>>;

namespace Pricing.Hexagon.ProductDiscounts.PrimaryPorts
{
    public interface IGetProductDiscountStrategiesUseCase
    {
        Task<IReadOnlyCollection<IProductGlobalDiscountStrategy>> GetGlobalDiscountStrategies(
            NonEmptyList<ProductId> productIds);
    }
}