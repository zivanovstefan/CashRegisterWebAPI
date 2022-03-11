using CashRegister.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface IBillService
    {
        ICollection<BillVM> GetAllBills();
        void Create(BillVM billVM);
        void Update(BillVM billVM);
        void Delete(ulong id);
        BillVM GetBillByID(ulong id);
    }
}
