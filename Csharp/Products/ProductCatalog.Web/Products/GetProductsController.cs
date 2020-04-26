using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.ApplicationServices.Products.UseCases;
using ProductCatalog.Domain.ProductAggregate;
using ProductCatalog.Web.Products.Dtos;

namespace ProductCatalog.Web.Products
{
    [ApiController]
    [Route(Routes.Products)]
    public class GetProductsController : ControllerBase
    {
        private readonly GetProductsUseCase _useCase;

        public GetProductsController(GetProductsUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _useCase.GetAsync();

            var dtos =
                products
                    .Select(p => new ProductWithIdDto(p))
                    .ToList();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var product = await _useCase.GetByIdAsync(new ProductId(id));

            return Ok(new ProductWithIdDto(product)); }

        public class ProductWithIdDto : ProductDto
        {
            public ProductWithIdDto(Product product)
            {
                Id = (Guid)product.Id;
                Name = (string) product.Name;
                Description = (string) product.Description;
                Dimension = 
                    new DimensionDto
                    {
                        Length = (int)product.Dimension.Length,
                        Width = (int)product.Dimension.Width,
                        Height = (int)product.Dimension.Height
                    };
                Weight = (int) product.Weight;
                CategoryIds =
                    product.CategoryIds
                        .Select(id => (int) id)
                        .ToList();
            }
            
            public Guid Id { get; }
        }
    }
}