using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Products.UseCases;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Products.Aggregate;
using ProductCatalog.Web.Products.Dtos;
using Shared.Core;
using Shared.Core.Validations;

namespace ProductCatalog.Web.Products
{
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
            var categories = dto.Validate();
            
            await _useCase.ChangeCategoriesAsync(new ProductId(id), categories.Value);
            return Ok();
        }

        public class Dto
        {
            public IReadOnlyCollection<int> CategoryIds { get; set; }

            public Validation<NonEmptyList<CategoryId>> Validate() =>
                ProductDto.ValidateCategories(CategoryIds);
            
            public class EmptyCategoriesValidationError : SimpleValidationError {}
        }
    }
}