using CashRegister.Application.Interfaces;
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
        public ActionResult<int> Exchange(int amount, string currency)
        {
            int result = 0;
            if (currency is "EUR")
            {
                result = amount / 100;
            }
            else if(currency is "USD")
            {
                result = amount / 50;
            }
            return result;
        }
    }
}
