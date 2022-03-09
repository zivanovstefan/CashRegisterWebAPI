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
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpPost("Add product")]
        public IActionResult Create([FromBody] ProductVM productVM)
        {
            _productService.Create(productVM);
                return Ok();
        }
        [HttpDelete("Delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            _productService.Delete(id);
            return Ok(id);
        }
        [HttpGet]
        public IEnumerable<ProductVM> GetAllBills()
        {
            return _productService.GetAllProducts();
        }
    }
}
