using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.UseCases;

namespace ProductCatalog.Web.Categories
{
    [ApiController]
    [Route(Routes.Categories)]
    public class GetCategoriesController : ControllerBase
    {
        private readonly GetCategoryUseCase _useCase;

        public GetCategoriesController(GetCategoryUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categories = await _useCase.GetAsync();

            var dtos =
                categories
                    .Select(c => new CategoryDto(c))
                    .ToList();

            return Ok(dtos);
        }
    

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _useCase.GetByIdAsync(new CategoryId(id));

            return Ok(new CategoryDto(category));
        }

        public class CategoryDto
        {
            public CategoryDto(Category category)
            {
                Id = (int)category.Id;
                Name = (string) category.Name;
            }
            
            public int Id { get; }
            public string Name { get; }
        }
    }
}