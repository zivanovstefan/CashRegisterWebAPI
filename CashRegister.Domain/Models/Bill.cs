using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Domain.Models
{
    public class Bill
    {
        [Key]
        public string BillNumber { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        public string CreditCardNumber { get; set; }
        public ICollection<ProductBill> BillProducts { get; set; }
    }
}
