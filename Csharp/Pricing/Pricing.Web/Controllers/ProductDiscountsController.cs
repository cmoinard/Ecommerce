using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pricing.Hexagon.ProductDiscounts.PrimaryPorts;
using Shared.Core.Extensions;
using Shared.Domain;
using Shared.Web;

namespace Pricing.Web.Controllers
{
    [ApiController]
    [Route(Routes.Discounts)]
    public class ProductDiscountsController : ControllerBase
    {
        private readonly IGetProductDiscountStrategiesUseCase _useCase;

        public ProductDiscountsController(IGetProductDiscountStrategiesUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery][ModelBinder(typeof(ListModelBinder<string>))] List<string> productIds)
        {
            var ids =
                productIds
                    .Select(id => new ProductId(Guid.Parse(id)))
                    .ToNonEmptyList();

            var strategies = await _useCase.GetGlobalDiscountStrategiesAsync(ids);
            
            // TODO: Convert to Dto
            
            return Ok(strategies);
        }
    }
}