using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Common
{
    public class Messages
    {
        //Bill error messages
        public const string Bill_Does_Not_Exist = "Bill with entered bill number does not exist";
        public const string EmptyDB = "There are no bills in database";
        //Product error messages
        public const string Product_Does_Not_Exist = "Product with entered product id does not exist";
        //ProductBill error messages
        public const string BillTotalPriceTooHigh = "Total price must be less than or equal to 5000";
        public const string BillProductErrorMessages = "Product with entered product id does not exist";
        //Currency exchange error messages
        public const string CurrencyNotSupported = "Currency not supported";
        public const string InvalidAmount = "Amount must be greater than 0";
    }
}
