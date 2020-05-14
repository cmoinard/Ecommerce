using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Categories.Aggregate;
using ProductCatalog.Domain.Categories.UseCases;

namespace ProductCatalog.Web.Categories
{
    [ApiController]
    [Route(Routes.Categories)]
    public class DeleteCategoryController : ControllerBase
    {
        private readonly DeleteCategoryUseCase _useCase;

        public DeleteCategoryController(DeleteCategoryUseCase useCase)
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