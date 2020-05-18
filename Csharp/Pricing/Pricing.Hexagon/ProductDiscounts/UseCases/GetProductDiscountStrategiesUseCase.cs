using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
using Pricing.Hexagon.ProductDiscounts.SecondaryPorts;
using Pricing.Hexagon.ProductDiscounts.Strategies;
using Pricing.Hexagon.Products.SecondaryPorts;
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
        private readonly IProductPricesRepository _productPriceRepository;

        public GetProductDiscountStrategiesUseCase(
            IProductWeightRepository productWeightRepository,
            IProductPricesRepository productPriceRepository)
        {
            _productWeightRepository = productWeightRepository;
            _productPriceRepository = productPriceRepository;
        }
        
        public async Task<IReadOnlyCollection<IProductGlobalDiscountStrategy>> GetGlobalDiscountStrategies(NonEmptyList<ProductId> productIds)
        {
            var products = await _productPriceRepository.GetAsync(productIds);
            var priceStrategy =
                new ProductGlobalPriceDiscountStrategy(
                    products.ToDictionary(p => p.Id, p => p.Price));
            
            var weightByProductId = await _productWeightRepository.GetWeightByProductIdAsync(productIds);
            var weightStrategy = new ProductGlobalWeightDiscountStrategy(weightByProductId);

            return new List<IProductGlobalDiscountStrategy> {priceStrategy, weightStrategy};
        }
    }
}