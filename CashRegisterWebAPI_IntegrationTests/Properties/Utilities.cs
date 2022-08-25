using CashRegister.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterWebAPI_IntegrationTests.Properties
{
    public class Utilities
    {
        public static void InitializeDB(DbContext db)
        {
            Bill bill1 = new Bill()
            {
                BillNumber = "200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 50,
                CreditCardNumber = "371449635398431"
            };
            Bill bill2 = new Bill()
            {
                BillNumber = "200000000007540220",
                PaymentMethod = "MasterCard",
                TotalPrice = 50,
                CreditCardNumber = "371449635398431"
            };
            List<Bill> billList = new List<Bill>();
            billList.Add(bill1);
            billList.Add(bill2);
            db.SaveChanges();
        }
    }
}
