using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.ViewModels
{
    public class ProductBillVM
    {
        [ForeignKey("Bill")]
        public string BillNumber { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductsPrice { get; set; }
    }
}
