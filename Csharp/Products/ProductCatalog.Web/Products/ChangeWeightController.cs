using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.CategoryAggregate;
using ProductCatalog.Domain.ProductAggregate;

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
            await _useCase.ChangeWeightAsync(new ProductId(id), dto.Weight);
            return Ok();
        }

        public class Dto
        {
            public int Weight { get; set; }
        }
    }
    
    [ApiController]
    [Route(Routes.Products)]
    public class ChangeCategoriesController : ControllerBase
    {
        private readonly ChangeCategoriesUseCase _useCase;

        public ChangeCategoriesController(ChangeCategoriesUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPatch("{id}/categories")]
        public async Task<IActionResult> ChangeCategoriesAsync(Guid id, [FromBody]Dto dto)
        {
            await _useCase.ChangeCategoriesAsync(
                new ProductId(id),
                dto.CategoryIds
                    .Select(id => new CategoryId(id))
                    .ToList());
            return Ok();
        }

        public class Dto
        {
            public IReadOnlyCollection<int> CategoryIds { get; set; }
        }
    }
}