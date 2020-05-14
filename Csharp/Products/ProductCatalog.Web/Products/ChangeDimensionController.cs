using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Products.UseCases;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Web.Products.Dtos;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeDimensionController : ControllerBase
    {
        private readonly ChangeDimensionUseCase _useCase;

        public ChangeDimensionController(ChangeDimensionUseCase useCase)
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