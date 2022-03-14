﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface ICurrencyExchangeService
    {
        ActionResult<int> Exchange(int amount, string currency);
    }
}