using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class AddProductsCommand : ProductBillCommand
    {
        public AddProductsCommand(string billNumber, int productId, decimal productsPrice)
        {
            BillNumber = billNumber;
            ProductId = productId;
            ProductsPrice = productsPrice;
        }
    }
}
