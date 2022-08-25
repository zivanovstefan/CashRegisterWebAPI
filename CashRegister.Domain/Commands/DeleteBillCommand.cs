using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Commands
{
    public class DeleteBillCommand : BillCommand
    {
        public string BillNumber { get; set; }
        public DeleteBillCommand(string billNumber)
        {
            BillNumber = billNumber;
        }
    }
}
