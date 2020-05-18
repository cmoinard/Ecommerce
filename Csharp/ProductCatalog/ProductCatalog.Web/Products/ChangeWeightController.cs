using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using Shared.Core.Validations;
using Shared.Domain;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeWeightController : ControllerBase
    {
        private readonly IChangeWeightUseCase _useCase;

        public ChangeWeightController(IChangeWeightUseCase useCase)
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
                Shared.Domain.Weight.TryGrams(Weight);
        }
    }
}