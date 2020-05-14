using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Products.UseCases;
using ProductCatalog.Domain.Products.Aggregate;
using Shared.Core.Validations;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeWeightController : ControllerBase
    {
        private readonly ChangeWeightUseCase _useCase;

        public ChangeWeightController(ChangeWeightUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPatch("{id}/weight")]
        public async Task<IActionResult> ChangeWeightAsync(Guid id, [FromBody]Dto dto)
        {
            var weight = dto.Validate();
            
            await _useCase.ChangeWeightAsync(new ProductId(id), weight.Value);
            return Ok();
        }

        public class Dto
        {
            public int Weight { get; set; }

            public Validation<Weight> Validate() =>
                Domain.Products.Aggregate.Weight.TryGrams(Weight);
        }
    }
}