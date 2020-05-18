using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Pricing.Hexagon.Products.PrimaryPorts;
using Pricing.Hexagon.Products.SecondaryPorts;
using Shared.Domain;

namespace Pricing.Hexagon.Products.UseCases
{
    public class SetProductProductPriceUseCase : ISetProductPriceUseCase
    {
        private readonly IProductPricesRepository _repository;

        public SetProductProductPriceUseCase(IProductPricesRepository repository)
        {
            _repository = repository;
        }
        
        public async Task SetPriceAsync(ProductId id, Price price)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                await _repository.SaveAsync(new ProductPrice(id, price));
            else
            {
                product.Price = price;
                await _repository.SaveAsync(product);
            }
        }
    }
}