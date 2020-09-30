using System.Linq;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.Aggregate;
using Pricing.Hexagon.ProductDiscounts.Aggregate.Strategies;
using Pricing.Hexagon.Products.SecondaryPorts;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.ProductDiscounts.ApplicationServices
{
    public class ProductPriceStrategyService
    {
        private readonly IProductPricesRepository _productPriceRepository;

        public ProductPriceStrategyService(IProductPricesRepository productPriceRepository)
        {
            _productPriceRepository = productPriceRepository;
        }

        public async Task<DiscountStrategy> GetPriceDiscountStrategyAsync(NonEmptyList<ProductId> productIds)
        {
            var products = await _productPriceRepository.GetAsync(productIds);
            
            return
                new DiscountStrategy(
                    new DiscountStrategyId(1),
                    new DiscountName("Price discount"),
                    new DiscountDescription("-25€ si montant > 500€"),
                    new ProductGlobalPriceDiscountStrategy(
                        products.ToDictionary(p => p.Id, p => p.Price)));
        }
    }
}