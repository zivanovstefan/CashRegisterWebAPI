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
        [HttpDelete("Delete bill product")]
        public IActionResult Delete([FromRoute]int id, int id1)
        {
            _productBillService.Delete(id, id1);
            return Ok();
        }
    }
}
