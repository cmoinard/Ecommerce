using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class DeleteProductController : ControllerBase
    {
        private readonly DeleteProductUseCase _useCase;

        public DeleteProductController(DeleteProductUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _useCase.DeleteAsync(new ProductId(id));
            return Ok();
        }
    }
}