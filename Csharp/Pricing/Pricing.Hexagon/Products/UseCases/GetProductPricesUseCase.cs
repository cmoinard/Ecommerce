using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Pricing.Hexagon.Products.PrimaryPorts;
using Pricing.Hexagon.Products.SecondaryPorts;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.Products.UseCases
{
    public class GetProductPricesUseCase : IGetProductPricesUseCase
    {
        private readonly IProductPricesRepository _repository;

        public GetProductPricesUseCase(IProductPricesRepository repository)
        {
            _repository = repository;
        }
        
        public Task<IReadOnlyCollection<ProductPrice>> GetPriceByProductIdAsync(NonEmptyList<ProductId> productIds) => 
            _repository.GetAsync(productIds);
    }
}