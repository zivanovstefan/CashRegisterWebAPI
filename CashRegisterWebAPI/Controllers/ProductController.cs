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
        [HttpGet("GetAllProducts")]
        public ActionResult<List<ProductVM>> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] ProductVM productVM)
        {
            if (productVM == null)
            {
                return BadRequest();
            }
            _productService.Create(productVM);
            return Ok();
        }
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] ProductVM productVM)
        {
            if (productVM == null)
            {
                return BadRequest();
            }
            _productService.Update(productVM);
            return Ok();
        }
        [HttpDelete("DeleteProduct{id}")]
        public ActionResult<bool> DeleteProduct([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var deletedProduct = _productService.Delete(id);

            return deletedProduct;
        }
    }
}
