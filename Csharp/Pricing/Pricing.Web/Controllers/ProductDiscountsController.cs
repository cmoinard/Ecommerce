using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pricing.Hexagon.ProductDiscounts.Aggregate;
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
            
            var dtos =
                strategies
                    .Select(s => DiscountStrategyDto.FromDomain(s))
                    .ToList();
            
            return Ok(dtos);
        }
    }

    public class DiscountStrategyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public static DiscountStrategyDto FromDomain(DiscountStrategy strategy) =>
            new DiscountStrategyDto
            {
                Id = (int)strategy.Id,
                Name = (string)strategy.Name,
                Description = (string)strategy.Description,
            };
    }
}