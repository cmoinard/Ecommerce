using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.Aggregate;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using Shared.Core.Validations;

namespace ProductCatalog.Web.Categories
{
    [ApiController]
    [Route(Routes.Categories)]
    public class CreateCategoryController : ControllerBase
    {
        private readonly ICreateCategoryUseCase _useCase;

        public CreateCategoryController(ICreateCategoryUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryDto dto)
        {
            var category = dto.Validate();
            
            await _useCase.CreateAsync(category.Value);

            return Ok();
        }
    
        public class CategoryDto
        {
            public string Name { get; set; }
            
            public Validation<UncreatedCategory> Validate()
            {
                var name = CategoryName.TryCreate(Name);

                return
                    name.IsValid
                        ? Validation.Valid(new UncreatedCategory(name.Value))
                        : Validation.Invalid<UncreatedCategory>(name.Errors);
            }
        }
    }
}