using CashRegister.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class ProductBillCommand : Command
    {
        public string BillNumber { get; set; }
        public int ProductId { get; set; }
        public int ProductsPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
