﻿using CashRegister.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface IProductService
    {
        ICollection<ProductVM> GetAllProducts();
    }
}
