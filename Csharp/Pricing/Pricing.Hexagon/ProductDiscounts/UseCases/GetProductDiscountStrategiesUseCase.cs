using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.Aggregate;
using Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies;
using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
using Pricing.Hexagon.ProductDiscounts.SecondaryPorts;
using Pricing.Hexagon.Products.SecondaryPorts;
using Shared.Core;
using Shared.Domain;

using DiscountStrategiesByProductId = 
    System.Collections.Generic.IReadOnlyDictionary<
        Shared.Domain.ProductId,
        System.Collections.Generic.IReadOnlyCollection<
            Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies.IProductGlobalDiscountStrategy>>;

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
        
        public async Task<IReadOnlyCollection<DiscountStrategy>> GetGlobalDiscountStrategiesAsync(NonEmptyList<ProductId> productIds)
        {
            var products = await _productPriceRepository.GetAsync(productIds);
            var priceGlobalStrategy =
                new ProductGlobalPriceDiscountStrategy(
                    products.ToDictionary(p => p.Id, p => p.Price));
            var priceStrategy =
                new DiscountStrategy(
                    new DiscountStrategyId(1),
                    new DiscountName("Price discount"),
                    new DiscountDescription("-25€ si montant > 500€"),
                    priceGlobalStrategy);
            
            var weightByProductId = await _productWeightRepository.GetWeightByProductIdAsync(productIds);
            var weightGlobalStrategy = new ProductGlobalWeightDiscountStrategy(weightByProductId);
            var weightStrategy =
                new DiscountStrategy(
                    new DiscountStrategyId(1),
                    new DiscountName("Price discount"),
                    new DiscountDescription("-5% si poids > 50Kg"),
                    weightGlobalStrategy);

            return new List<DiscountStrategy>
            {
                priceStrategy, 
                weightStrategy
            };
        }
    }
}