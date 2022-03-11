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
        [HttpGet("Get all products")]
        public IEnumerable<ProductVM> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }
    }
}
