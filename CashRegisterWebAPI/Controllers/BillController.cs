using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CashRegister.API.Validator;
using FluentValidation;
using CashRegister.Application.ErrorModels;
using CashRegister.Domain.Common;

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
        [HttpGet("Bills")]
        public IEnumerable<BillVM> GetBills()
        {
            return _billService.GetAllBills();
        }
        [HttpGet("GetAllBills")]
        public IEnumerable<BillVM> GetAllBills()
        {
            return _billService.GetAllBills();
        }
        [HttpPost("AddBill")]
        public ActionResult CreateBill([FromBody] BillVM billVM)
        {
            if (billVM == null)
            {
                return BadRequest();
            }
                _billService.Create(billVM);
                return Ok(billVM);
        }
        [HttpPut("UpdateBill")]
        public ActionResult UpdateBill([FromBody]BillVM billVM)
        {
            if (billVM == null)
            {
                return BadRequest();
            }
            _billService.Update(billVM);
            return Ok(billVM);
        }
        [HttpDelete("DeleteBill{billNumber}")]
        public ActionResult DeleteBill([FromRoute] string billNumber)
        {
            _billService.Delete(billNumber);
            return Ok(billNumber);
        }
        [HttpGet("GetBillByBillNumber{billNumber}")]
        public ActionResult GetBillByBillNumber([FromRoute] string billNumber)
        {
            var bill = _billService.GetBillByID(billNumber);
            if (billNumber == null)
            {
                return BadRequest(billNumber);
            }
            if (billNumber == "")
            {
                return BadRequest();
            }
            if (bill == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel();
                {
                    errorResponse.ErrorMessage = Messages.Bill_Does_Not_Exist;
                    errorResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                };
                return BadRequest(errorResponse);
            }
            return Ok(bill);
        }
    }
}
