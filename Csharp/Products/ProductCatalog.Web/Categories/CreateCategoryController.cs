using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Categories;
using ProductCatalog.ApplicationServices.Categories.UseCases;

namespace ProductCatalog.Web.Categories
{
    [ApiController]
    [Route(Routes.Categories)]
    public class CreateCategoryController : ControllerBase
    {
        private readonly CreateCategoryUseCase _useCase;

        public CreateCategoryController(CreateCategoryUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDto dto)
        {
            await _useCase.CreateAsync(
                new UnvalidatedCategoryState(dto.Name));

            return Ok();
        }
    
        public class CategoryDto
        {
            public string Name { get; set; }
        }
    }
}