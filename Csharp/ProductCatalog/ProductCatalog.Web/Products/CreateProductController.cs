using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Hexagon.Products.UseCases;
using ProductCatalog.Web.Products.Dtos;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Validations;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class CreateProductController : ControllerBase
    {
        private readonly ICreateProductUseCase _useCase;

        public CreateProductController(ICreateProductUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDto dto)
        {
            try
            {
                var product = dto.Validate();

                await _useCase.CreateAsync(product.Value);
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(
                    "Product is invalid :\n" +
                    e.Errors
                        .Select(error => $"\t- {GetErrorLabel(error)}")
                        .JoinWith("\n"));
            }
            catch (ProductNameAlreadyExistsException)
            {
                return BadRequest($"Product name \"{dto.Name}\" already exists");
            }
        }

        private string GetErrorLabel(ValidationError error) =>
            error switch
            {
                ProductName.EmptyValidationError _ => "Empty name",
                ProductName.InvalidCharactersValidationError _ => "Invalid characters in name",
                ProductName.ExceedsMaxLengthValidationError _ => $"Name exceeds {ProductName.MaxLength} characters",
                
                ProductDescription.EmptyValidationError _ => "Empty description",
                ProductDescription.ExceedsMaxLengthValidationError _ => $"Description exceeds {ProductDescription.MaxLength} characters",
                
                DimensionDto.HeightValidationError _ => "Height is negative",
                DimensionDto.LengthValidationError _ => "Length is negative",
                DimensionDto.WidthValidationError _ => "Width is negative",
                
                Weight.NegativeSizeValidationError _ => "Weight is negative",
                
                ProductDto.EmptyCategoriesValidationError _ => "No category",
                
                _ => throw new NotSupportedException()
            };
    }
}