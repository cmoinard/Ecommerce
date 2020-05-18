using System.Collections.Generic;
using System.Threading.Tasks;
using Pricing.Hexagon.Products.Aggregate;
using Shared.Core;
using Shared.Domain;

namespace Pricing.Hexagon.Products.PrimaryPorts
{
    public interface IGetProductPricesUseCase
    {
        Task<IReadOnlyCollection<ProductPrice>> GetPriceByProductIdAsync(NonEmptyList<ProductId> productIds);
    }
}