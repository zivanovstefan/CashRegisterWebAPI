﻿using CashRegister.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class UpdateCommand : Command
    {
        public string BillNumber { get; set; }
        public string PaymentMethod { get; set; }
        public string CreditCardNumber { get; set; }
    }
}