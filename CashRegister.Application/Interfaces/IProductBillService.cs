using CashRegister.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface IProductBillService
    {
        ICollection<ProductBillVM> GetAllBillProducts();
        void AddProductToBill(ProductBillVM productBillVM);
        void Delete(int id1, int id2);
    }
}
