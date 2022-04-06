using CashRegister.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<int> Exchange(int amount, string currency)
        {
            int result = 0;
            if (currency is "EUR")
            {
                return  result = amount / 100;
            }
            else if (currency is "USD")
            {
                return result = amount / 50;
            }
            else throw new Exception("Currency not supported");
        }
    }
}
