﻿using CashRegister.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class UpdateBillCommand : BillCommand
    {
        public UpdateBillCommand(int billNumber, string paymentMethod, decimal totalPrice, int creditCardNumber)
        {
            BillNumber = billNumber;
            PaymentMethod = paymentMethod;
            TotalPrice = totalPrice;
            CreditCardNumber = creditCardNumber;
        }
    }
}
