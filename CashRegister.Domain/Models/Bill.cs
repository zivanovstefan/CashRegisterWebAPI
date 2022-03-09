﻿using System;
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
        public int CreditCardNumber { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
