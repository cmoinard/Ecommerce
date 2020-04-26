using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeDescriptionController : ControllerBase
    {
        private readonly ChangeDescriptionUseCase _useCase;

        public ChangeDescriptionController(ChangeDescriptionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPatch("{id}/description")]
        public async Task<IActionResult> ChangeDescriptionAsync(Guid id, [FromBody]Dto dto)
        {
            await _useCase.ChangeDescriptionAsync(new ProductId(id), dto.Description);
            return Ok();
        }

        public class Dto
        {
            public string Description { get; set; }
        }
    }
}