using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Categories;
using ProductCatalog.Hexagon.Categories.PrimaryPorts;
using ProductCatalog.Hexagon.Categories.UseCases;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.Web.Categories
{
    [Route(Routes.Categories)]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGetCategoriesUseCase _getUseCase;
        private readonly IDeleteCategoryUseCase _deleteUseCase;
        private readonly ICreateCategoryUseCase _createUseCase;

        public CategoriesController(
            IGetCategoriesUseCase getUseCase,
            IDeleteCategoryUseCase deleteUseCase,
            ICreateCategoryUseCase createUseCase)
        {
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
            _createUseCase = createUseCase;
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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CreateCategoryDto category)
        {
            try
            {
                var categoryId = await _createUseCase.CreateAsync(category.CategoryName);
                return Ok(categoryId);
            }
            catch (CategoryNameAlreadyExistsException ex)
            {
                return BadRequest($"Name {ex.CategoryName} already exists");
            }
            catch (ValidationException ex)
            {
                return BadRequest(
                    "Name is invalid :\n" +
                    ex.Errors
                        .Select(e => $"\t- {GetValidationErrorLabel(e)}")
                        .JoinWith("\n"));
            }
        }

        private static string GetValidationErrorLabel(ValidationError error) =>
            error switch
            {
                CategoryName.EmptyValidationError _ => "Nom vide",  
                CategoryName.ExceedsMaxLengthValidationError _ => $"Nom excède {CategoryName.MaxLength} caractères",
                _ => throw new NotSupportedException()
            };
    }
}