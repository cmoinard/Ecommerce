using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.Aggregate;
using Pricing.Hexagon.ProductDiscounts.ApplicationServices;
using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.UseCases
{
    public class GetProductDiscountStrategiesUseCase : IGetProductDiscountStrategiesUseCase
    {
        private readonly ProductWeightStrategyService _productWeightStrategyService;
        private readonly ProductPriceStrategyService _productPriceStrategyService;

        public GetProductDiscountStrategiesUseCase(
            ProductWeightStrategyService productWeightStrategyService,
            ProductPriceStrategyService productPriceStrategyService)
        {
            _productPriceStrategyService = productPriceStrategyService;
            _productWeightStrategyService = productWeightStrategyService;
        }
        
        public async Task<IReadOnlyCollection<DiscountStrategy>> GetGlobalDiscountStrategiesAsync(NonEmptyList<ProductId> productIds)
        {
            var priceStrategy = await _productPriceStrategyService.GetPriceDiscountStrategyAsync(productIds);
            var weightStrategy = await _productWeightStrategyService.GetWeightDiscountStrategyAsync(productIds);

            return new List<DiscountStrategy>
            {
                priceStrategy, 
                weightStrategy
            };
        }
    }
}