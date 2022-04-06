using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CashRegister.API.Validator;
using FluentValidation;
using CashRegister.Application.ErrorModels;
using CashRegister.Domain.Common;
using CashRegister.Application.ErrorModels;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet("Get all bills")]
        public IEnumerable<BillVM> GetAllBills()
        {
            return _billService.GetAllBills();
        }
        [HttpPost("Add bill")]
        public ActionResult CreateBill([FromBody] BillVM billVM)
        {
            if (billVM == null)
            {
                return BadRequest();
            }
                _billService.Create(billVM);
                return Ok(billVM);
        }
        [HttpPut("Update bill")]
        public void UpdateBill([FromBody]BillVM billVM)
        {
            _billService.Update(billVM);
        }
        [HttpDelete("Delete bill{billNumber}")]
        public ActionResult DeleteBill([FromRoute] string billNumber)
        {
            _billService.Delete(billNumber);
            return Ok(billNumber);
        }
        [HttpGet("GetBillByBillNumber{billNumber}")]
        public ActionResult<BillVM> GetBillByBillNumber([FromRoute] string billNumber)
        {
            var bill = _billService.GetBillByID(billNumber);
            if (bill == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel();
                {
                    errorResponse.ErrorMessage = Messages.Bill_Does_Not_Exist;
                    errorResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                };
                return NotFound(errorResponse);
            }
            return bill;
        }
    }
}
