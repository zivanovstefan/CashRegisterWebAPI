using CashRegister.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Interfaces
{
    public interface IProductBillRepository
    {
        void Add(ProductBill productBill);
        void Update(ProductBill productBill);
        IEnumerable<ProductBill> GetProductBills();
        void Delete(ProductBill productBill);
    }
}
