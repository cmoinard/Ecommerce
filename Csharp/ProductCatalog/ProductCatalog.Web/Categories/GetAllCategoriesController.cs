using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;

namespace ProductCatalog.Web.Categories
{
    [Route(Routes.Categories)]
    [ApiController]
    public class GetAllCategoriesController : ControllerBase
    {
        private readonly IGetCategoriesUseCase _useCase;

        public GetAllCategoriesController(IGetCategoriesUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categories = await _useCase.GetAllAsync();
            return Ok(categories);
        }
    }
}