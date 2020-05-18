using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using ProductCatalog.Web.Products.Dtos;

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
            var product = dto.Validate();
            
            await _useCase.CreateAsync(product.Value);
            return Ok();
        }
    }
}