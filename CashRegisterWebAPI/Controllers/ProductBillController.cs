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
        public IEnumerable<ProductBillVM> GetAllBillProducts()
        {
           return _productBillService.GetAllBillProducts();
        }
        [HttpPost("Add product to bill")]
        public ActionResult CreateBill([FromBody] ProductBillVM productBillVM) //ActionResult<bool>
        {
            if (productBillVM == null)
            {
                return BadRequest();
            }
            _productBillService.AddProductToBill(productBillVM);
            return Ok(productBillVM);
            //return true;
        }
        [HttpDelete("Delete bill product{billNumber}, {productId}")]
        public ActionResult<bool> Delete([FromRoute]string billNumber, int productId, int quantity)
        {
            //if ((billNumber == "") && (productId == 0) && (quantity == 0)){
            if (billNumber == "")
            {
                return false;
            }
            if (productId == 0)
            {
                return false;
            }
            if (quantity == 0)
            {
                return false;
            }
            _productBillService.DeleteProductsFromBill(billNumber, productId, quantity);
            return true;
        }
    }
}
