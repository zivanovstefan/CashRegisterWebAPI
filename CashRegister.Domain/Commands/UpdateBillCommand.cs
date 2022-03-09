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
        public UpdateBillCommand(int id, string paymentMethod, decimal totalPrice, int creditCardNumber)
        {
            Id = id;
            PaymentMethod = paymentMethod;
            TotalPrice = totalPrice;
            CreditCardNumber = creditCardNumber;
        }
    }
}
