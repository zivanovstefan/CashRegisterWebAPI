using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterWebAPI_IntegrationTests
{
    static class TestData
    {
        public static void DataForIntegrationTests(CashRegisterDBContext context)
        {
            var productBill = new ProductBill()
            {
                BillNumber = "105008123123123173",
                ProductId = 1,
                ProductQuantity = 5,
                ProductsPrice = 250
            };
            context.BillProducts.Add(productBill);

            var productBills = new List<ProductBill>();
            productBills.Add(productBill);

            var product1 = new Product()
            {
                Id = 1,
                Name = "Book",
                Price = 10,
                BillProducts = productBills
            };
            var product2 = new Product()
            {
                Id = 2,
                Name = "Computer",
                Price = 500
            };
            context.Products.Add(product1);
            context.Products.Add(product2);

            var bill = new Bill()
            {
                BillNumber = "105008123123123173",
                TotalPrice = 150,
                CreditCardNumber = "4532562104787989",
                BillProducts = productBills
            };
            context.Bills.Add(bill);
            context.SaveChanges();
        }
    }
}
