using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Web.Products.Dtos;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class CreateProductController : ControllerBase
    {
        private readonly CreateProductUseCase _useCase;

        public CreateProductController(CreateProductUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductDto dto)
        {
            var product = dto.Validate();
            
            await _useCase.CreateAsync(product.Value);
            return Ok();
        }
    }
}