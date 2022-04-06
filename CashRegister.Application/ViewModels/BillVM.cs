using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Models;

namespace CashRegister.Application.ViewModels
{
    public class BillVM
    {
        public string BillNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int TotalPrice { get; set; }
        public string CreditCardNumber { get; set; }
        public ICollection<ProductBillVM> BillProducts { get; set; }
    }
}
