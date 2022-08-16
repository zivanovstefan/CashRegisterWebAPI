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
        public const string BillAlreadyExist = "There are no bills in database";
        //Product error messages
        public const string Product_Already_Exist = "Product with inserted id already exists.";
        public const string Product_Does_Not_Exist = "Product with inserted id does not exist.";
        public const string Products_Table_Is_Empty = "Products table is empty.";
        //ProductBill error messages
        public const string BillTotalPriceTooHigh = "Total price must be less than or equal to 5000";
        public const string BillProductErrorMessage = "BillProduct with entered product id does not exist";
        public const string QuantityTooHigh = "Entered product quantity is bigger than product quantity in database";
        public const string Bill_Has_Zero_Products = "There are no bill products";
        //Currency exchange error messages
        public const string CurrencyNotSupported = "Currency not supported";
        public const string InvalidAmount = "Amount must be greater than 0";
        //User error messages 
        public const string User_Not_Found = "User with entered id was not found";
        public const string User_Creation_Error = "Error occured while creating new user, please try again.";
    }
}
