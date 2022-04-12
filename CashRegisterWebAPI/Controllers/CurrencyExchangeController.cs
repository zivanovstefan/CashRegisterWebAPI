using CashRegister.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public CurrencyExchangeController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
        }
        [HttpGet("Exchange{amount},{currency}")]
        public ActionResult<int> Exchange([FromRoute] int amount, string currency)
        {
            var result = _currencyExchangeService.Exchange(amount, currency);
            return result;
        }
    }
}
