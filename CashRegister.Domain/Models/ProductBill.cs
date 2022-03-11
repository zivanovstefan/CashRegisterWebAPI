using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Domain.Models
{
    public class ProductBill
    {
        public int ProductQuantity { get; set; }
        public decimal ProductsPrice { get; set; }

        [ForeignKey("Bill")]
        public int BillNumber { get; set; }
        public Bill Bill { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
