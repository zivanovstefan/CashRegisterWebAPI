using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class CreateBillCommand : BillCommand
    {
        public CreateBillCommand(string billNumber, string paymentMethod, decimal totalPrice, string creditCardNumber)
        {
            BillNumber = billNumber;
            PaymentMethod = paymentMethod;
            TotalPrice = totalPrice;
            CreditCardNumber = creditCardNumber;
        }
    }
}
