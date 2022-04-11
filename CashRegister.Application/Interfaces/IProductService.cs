using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductVM> GetAllProducts();
        ActionResult<bool> Create(ProductVM productVM);
        ActionResult<bool> Update(ProductVM productVM);
        ActionResult<bool> Delete(int id);
    }
}
