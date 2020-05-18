using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Web.Products.Dtos;
using Shared.Domain;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeDimensionController : ControllerBase
    {
        private readonly IChangeDimensionUseCase _useCase;

        public ChangeDimensionController(IChangeDimensionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPatch("{id}/dimension")]
        public async Task<IActionResult> ChangeDimensionAsync(Guid id, [FromBody]DimensionDto dto)
        {
            var dimension = dto.Validate();
            
            await _useCase.ChangeDimensionsAsync(new ProductId(id), dimension.Value);
            return Ok();
        }
    }
}