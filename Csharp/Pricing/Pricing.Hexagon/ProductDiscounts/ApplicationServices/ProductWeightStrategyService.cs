using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.Aggregate;
using Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies;
using Pricing.Hexagon.ProductDiscounts.SecondaryPorts;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.ApplicationServices
{
    public class ProductWeightStrategyService
    {
        private readonly IProductWeightRepository _productWeightRepository;

        public ProductWeightStrategyService(IProductWeightRepository productWeightRepository)
        {
            _productWeightRepository = productWeightRepository;
        }

        public async Task<DiscountStrategy> GetWeightDiscountStrategyAsync(NonEmptyList<ProductId> productIds)
        {
            var weightByProductId = await _productWeightRepository.GetWeightByProductIdAsync(productIds);

            return
                new DiscountStrategy(
                    new DiscountStrategyId(1),
                    new DiscountName("Price discount"),
                    new DiscountDescription("-5% si poids > 50Kg"),
                    new ProductGlobalWeightDiscountStrategy(weightByProductId));
        }
    }
}