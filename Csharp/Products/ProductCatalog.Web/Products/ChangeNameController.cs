using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeNameController : ControllerBase
    {
        private readonly ChangeNameUseCase _useCase;

        public ChangeNameController(ChangeNameUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPatch("{id}/name")]
        public async Task<IActionResult> ChangeNameAsync(Guid id, [FromBody]Dto dto)
        {
            await _useCase.ChangeNameAsync(new ProductId(id), dto.Name);
            return Ok();
        }

        public class Dto
        {
            public string Name { get; set; }
        }
    }
}