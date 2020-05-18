using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using Shared.Domain;

namespace ProductCatalog.Web.Categories
{
    [ApiController]
    [Route(Routes.Categories)]
    public class DeleteCategoryController : ControllerBase
    {
        private readonly IDeleteCategoryUseCase _useCase;

        public DeleteCategoryController(IDeleteCategoryUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpDelete]
        public async Task<IActionResult> CreateAsync(int id)
        {
            await _useCase.DeleteAsync(new CategoryId(id));

            return Ok();
        }
    }
}