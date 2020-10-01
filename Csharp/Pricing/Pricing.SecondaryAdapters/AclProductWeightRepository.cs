using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pricing.Hexagon.ProductDiscounts.SecondaryPorts;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using Shared.Core;
using Shared.Domain;

namespace Pricing.SecondaryAdapters
{
    public class AclProductWeightRepository : IProductWeightRepository
    {
        private readonly IGetProductsUseCase _getProductsUseCase;

        public AclProductWeightRepository(IGetProductsUseCase getProductsUseCase)
        {
            _getProductsUseCase = getProductsUseCase;
        }
        
        public async Task<IReadOnlyDictionary<ProductId, Weight>> GetWeightByProductIdAsync(NonEmptyList<ProductId> productIds)
        {
            var products = await _getProductsUseCase.GetAsync(productIds);

            return
                products
                    .ToDictionary(
                        p => p.Id,
                        p => p.Weight);
        }
    }
}