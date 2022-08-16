using CashRegister.Application.ErrorModels;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Common;
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
        public ActionResult<double> Exchange(double amount, string currency)
        {
            if (amount <= 0)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.InvalidAmount,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return new BadRequestObjectResult(errorResponse);
            }
            double result = 0;
            if (currency is "EUR")
            {
                result = amount / 117;
                return Math.Round(result, 2);
            }
            else if (currency is "USD")
            {
                result = amount / 112;
                return Math.Round(result, 2);
            }
            else
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.CurrencyNotSupported,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return new BadRequestObjectResult(errorResponse);
            }
        }
    }
}
