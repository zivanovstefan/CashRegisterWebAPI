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
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
        IQueryable<Product> Products { get; set; }
    }
}
