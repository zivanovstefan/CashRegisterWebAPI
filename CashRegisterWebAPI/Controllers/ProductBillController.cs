﻿using CashRegister.Application.Interfaces;
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
        [HttpPost("AddProductToBill")]
        public ActionResult<bool> CreateBill([FromBody] ProductBillVM productBillVM) //ActionResult<bool>
        {
            if (productBillVM == null)
            {
                return BadRequest();
            }
            return _productBillService.AddProductToBill(productBillVM);
            //return Ok(productBillVM);
        }
        [HttpDelete("Remove product from bill{billNumber}, {productId}")]
        public ActionResult<bool> Delete([FromRoute]string billNumber, int productId, int quantity)
        {
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
