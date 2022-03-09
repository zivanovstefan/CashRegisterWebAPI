using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;
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
        public IActionResult CreateBill([FromBody] BillVM billVM)
        {
            if (billVM == null)
                return BadRequest();

            _billService.Create(billVM);
            return Ok(billVM);
        }
        [HttpPatch("Update bill")]
        public IActionResult UpdateBill([FromBody] BillVM billVM)
        {
            if (billVM == null)
                return BadRequest();
            _billService.Update(billVM);
            return Ok(billVM);
        }
        [HttpDelete("Delete bill")]
        public IActionResult DeleteBill([FromRoute] int id)
        {
            _billService.Delete(id);
            return Ok(id);
        }
        [HttpGet("GetBillByID")]
        public BillVM GetBillByID([FromRoute] int id)
        {
            return _billService.GetBillByID(id);
        }
    }
}
