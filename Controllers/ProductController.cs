using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiProjectsApplicationsAPI.Core.Dto;
using MultiProjectsApplicationsAPI.Core.Interfaces;

namespace MultiProjectsApplicationsAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProductsAsync")]
        public async Task<IActionResult> GetAllProductsAsync() => Ok(await _productService.GetAllProductsAsync());

        [HttpGet("GetProductByIdAsync/{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost("AddProductAsync")]
        public async Task<IActionResult> AddProductAsync(ProductDto productDto)
        {
            await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductByIdAsync), new { id = productDto.Id }, productDto);
        }

        [HttpPut("UpdatProductAsync/{id}")]
        public async Task<IActionResult> UpdatProductAsync(int id, ProductDto productDto)
        {
            if (id != productDto.Id) return BadRequest();
            await _productService.UpdatProductAsync(productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProductAsync(ProductDto productDto)
        {
            await _productService.RemoveProductAsync(productDto);
            return NoContent();
        }
    }
}
