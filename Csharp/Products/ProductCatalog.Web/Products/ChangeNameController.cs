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
            var name = dto.Validate();
            
            await _useCase.ChangeNameAsync(new ProductId(id), name.Value);
            
            return Ok();
        }

        public class Dto
        {
            public string Name { get; set; }

            public Validation<ProductName> Validate() =>
                ProductName.TryCreate(Name);
        }
    }
}