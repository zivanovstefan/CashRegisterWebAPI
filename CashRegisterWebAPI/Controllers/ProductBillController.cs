using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBillController : ControllerBase
    {
        private readonly IProductBillService _productBillService;
        public ProductBillController(IProductBillService productBillService)
        {
            _productBillService = productBillService;
        }
        [HttpGet("Get all bill products")]
        public ICollection<ProductBillVM> GetAllBillProducts()
        {
           return _productBillService.GetAllBillProducts();
        }
        [HttpPost("Add product to bill")]
        public IActionResult CreateBill([FromBody] ProductBillVM productBillVM)
        {
            _productBillService.AddProductToBill(productBillVM);
            return Ok();
        }
        [HttpDelete("Delete bill product{billNumber}, {productId}")]
        public IActionResult Delete([FromRoute]string billNumber, int productId)
        {
            _productBillService.Delete(billNumber, productId);
            return Ok();
        }
    }
}
