using Microsoft.AspNetCore.Mvc;
using System.Net;
using CashRegister.Domain.Models;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Get all products")]
        public IEnumerable<ProductVM> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }
        [HttpPost("Create product")]
        public IActionResult CreateProduct([FromBody] ProductVM productVM)
        {
            if (productVM == null)
            {
                return BadRequest();
            }
            _productService.Create(productVM);
            return Ok();
        }
        [HttpPut("Update product")]
        public IActionResult UpdateProduct([FromBody] ProductVM productVM)
        {
            if (productVM == null)
            {
                return BadRequest();
            }
            _productService.Update(productVM);
            return Ok();
        }
        [HttpDelete("Delete product{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.Delete(id);
            return Ok(id);
        }
    }
}
