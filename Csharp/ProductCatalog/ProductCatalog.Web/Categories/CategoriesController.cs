using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using Shared.Core.Exceptions;

namespace ProductCatalog.Web.Categories
{
    [Route(Routes.Categories)]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGetCategoriesUseCase _getUseCase;
        private readonly IDeleteCategoryUseCase _deleteUseCase;

        public CategoriesController(
            IGetCategoriesUseCase getUseCase,
            IDeleteCategoryUseCase deleteUseCase)
        {
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var categories = await _getUseCase.GetAllAsync();
            return Ok(categories);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string categoryId)
        {
            try
            {
                var id = new CategoryId(Guid.Parse(categoryId));

                await _deleteUseCase.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException<CategoryId>)
            {
                return Ok();
            }
            catch (FormatException)
            {
                return BadRequest("Invalid categoryId format");
            }
        }
    }
}