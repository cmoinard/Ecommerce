using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using Shared.Core.Exceptions;
using Shared.Core.Validations;
using Shared.Domain;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeDescriptionController : ControllerBase
    {
        private readonly IChangeDescriptionUseCase _useCase;

        public ChangeDescriptionController(IChangeDescriptionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPatch("{id}/description")]
        public async Task<IActionResult> ChangeDescriptionAsync(Guid id, [FromBody]Dto dto)
        {
            try
            {
                var description = dto.Validate();

                await _useCase.ChangeDescriptionAsync(new ProductId(id), description.Value);
                return Ok();
            }
            catch (NotFoundException<ProductId>)
            {
                return NotFound();
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
        }

        public class Dto
        {
            public string Description { get; set; }

            public Validation<ProductDescription> Validate() =>
                ProductDescription.TryCreate(Description);
        }
    }
}