using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Models;

namespace CashRegister.Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
    }
}
