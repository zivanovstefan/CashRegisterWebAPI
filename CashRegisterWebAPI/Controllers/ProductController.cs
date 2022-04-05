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
        public IActionResult Create([FromBody] ProductVM productVM)
        {
            if (productVM == null)
                return BadRequest();
            _productService.Create(productVM);
            return Ok();
        }
        [HttpPut("Update product")]
        public void UpdateProduct([FromBody] ProductVM productVM)
        {
            _productService.Update(productVM);
        }
        [HttpDelete("Delete product{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.Delete(id);
            return Ok(id);
        }
    }
}
