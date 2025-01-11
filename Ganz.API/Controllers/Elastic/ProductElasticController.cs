using Ganz.Application.Dtos.ElasticDto;
using Ganz.Application.Services.Elastic;
using Microsoft.AspNetCore.Mvc;

namespace Ganz.API.Controllers.Elastic
{
    [ApiController]
    [Route("api/products")]
    public class ProductElasticController : ControllerBase
    {
        private readonly ProductElasticService _productService;

        public ProductElasticController(ProductElasticService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ElProduct product)
        {
            await _productService.AddProductAsync(product);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string query)
        {
            var products = await _productService.SearchProductsAsync(query);
            return Ok(products);
        }
    }

}
