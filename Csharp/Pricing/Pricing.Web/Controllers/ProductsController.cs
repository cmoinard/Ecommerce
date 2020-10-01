using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pricing.Hexagon.Products.Aggregate;
using Pricing.Hexagon.Products.PrimaryPorts;
using Shared.Core.Extensions;
using Shared.Domain;
using Shared.Web;

namespace Pricing.Web.Controllers
{
    [ApiController]
    [Route(Routes.Products)]
    public class ProductsController : ControllerBase
    {
        private readonly IGetProductPricesUseCase _getUseCase;
        private readonly ISetProductPriceUseCase _setUseCase;

        public ProductsController(
            IGetProductPricesUseCase getUseCase,
            ISetProductPriceUseCase setUseCase)
        {
            _getUseCase = getUseCase;
            _setUseCase = setUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery][ModelBinder(typeof(ListModelBinder<string>))] List<string> productIds)
        {
            var ids =
                productIds
                    .Select(id => new ProductId(Guid.Parse(id)))
                    .ToNonEmptyList();

            var prices = await _getUseCase.GetPriceByProductIdAsync(ids);
            var dtos =
                prices
                    .Select(p => ProductDto.FromDomain(p))
                    .ToList();

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ProductDto dto)
        {
            await _setUseCase.SetPriceAsync(
                new ProductId(new Guid(dto.Id)),
                new Price(dto.Price));

            return Ok();
        }
    }

    public class ProductDto
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        
        public static ProductDto FromDomain(ProductPrice product) =>
            new ProductDto
            {
                Id = ((Guid)product.Id).ToString(),
                Price = (decimal)product.Price
            };
    }
}