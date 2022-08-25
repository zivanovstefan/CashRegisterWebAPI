using CashRegister.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class UpdateBillCommand : UpdateCommand
    {
        public UpdateBillCommand(string billNumber, string paymentMethod, string creditCardNumber)
        {
            BillNumber = billNumber;
            PaymentMethod = paymentMethod;
            CreditCardNumber = creditCardNumber;
        }
    }
}
