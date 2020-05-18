// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Pricing.Hexagon.Discounts;
// using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
// using Pricing.Hexagon.Products.Aggregate;
// using Pricing.Hexagon.Products.PrimaryPorts;
// using Pricing.Hexagon.Products.SecondaryPorts;
// using Shared.Core;
// using Shared.Domain;
//
// namespace Pricing.Hexagon.Products.UseCases
// {
//     public class GetProductPricesUseCase : IGetProductPricesUseCase
//     {
//         private readonly IProductPricesRepository _repository;
//         private readonly IGetProductDiscountStrategiesUseCase _getDiscountStrategies;
//
//         public GetProductPricesUseCase(
//             IProductPricesRepository repository,
//             IGetProductDiscountStrategiesUseCase getDiscountStrategies)
//         {
//             _repository = repository;
//             _getDiscountStrategies = getDiscountStrategies;
//         }
//         
//         public async Task<IReadOnlyDictionary<ProductId, Price>> GetPriceByProductIdAsync(NonEmptyList<ProductId> productIds)
//         {
//             var productPrices = await _repository.GetPriceByProductIdAsync(productIds);
//             var priceByProductId = productPrices.ToDictionary(p => p.Id);
//                 
//             var discountStrategiesByProductId = await _getDiscountStrategies.GetDiscountStrategiesByProductIds(productIds);
//
//             return
//                 productIds
//                     .ToDictionary(
//                         pId => pId,
//                         pId =>
//                             discountStrategiesByProductId[pId]
//                                 .Select(s => s.GetDiscount(productPrices))
//                                 .ApplyOn(priceByProductId[pId].Price));
//         }
//     }
// }