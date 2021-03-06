using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Hexagon.Products.Aggregate;
using ProductCatalog.Hexagon.Products.PrimaryPorts;
using Shared.Core.Exceptions;
using Shared.Domain;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class DeleteProductController : ControllerBase
    {
        private readonly IDeleteProductUseCase _useCase;

        public DeleteProductController(IDeleteProductUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _useCase.DeleteAsync(new ProductId(id));
                return Ok();
            }
            catch (NotFoundException<ProductId>)
            {
                return Ok();
            }
        }
    }
}