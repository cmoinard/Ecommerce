using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
using Pricing.Hexagon.ProductDiscounts.SecondaryPorts;
using Pricing.Hexagon.ProductDiscounts.Strategies;
using Shared.Core;
using Shared.Domain;

using DiscountStrategiesByProductId = 
    System.Collections.Generic.IReadOnlyDictionary<
        Shared.Domain.ProductId,
        System.Collections.Generic.IReadOnlyCollection<
            Pricing.Hexagon.ProductDiscounts.Strategies.IProductGlobalDiscountStrategy>>;

namespace Pricing.Hexagon.ProductDiscounts.UseCases
{
    public class GetProductDiscountStrategiesUseCase : IGetProductDiscountStrategiesUseCase
    {
        private readonly IProductWeightRepository _productWeightRepository;
        private readonly IProductPriceRepository _productPriceRepository;

        public GetProductDiscountStrategiesUseCase(
            IProductWeightRepository productWeightRepository,
            IProductPriceRepository productPriceRepository)
        {
            _productWeightRepository = productWeightRepository;
            _productPriceRepository = productPriceRepository;
        }
        
        public async Task<IReadOnlyCollection<IProductGlobalDiscountStrategy>> GetGlobalDiscountStrategies(NonEmptyList<ProductId> productIds)
        {
            var priceByProductId = await _productPriceRepository.GetPriceByProductIdAsync(productIds);
            var priceStrategy = new ProductGlobalPriceDiscountStrategy(priceByProductId);
            
            var weightByProductId = await _productWeightRepository.GetWeightByProductIdAsync(productIds);
            var weightStrategy = new ProductGlobalWeightDiscountStrategy(weightByProductId);

            return new List<IProductGlobalDiscountStrategy> {priceStrategy, weightStrategy};
        }
    }
}