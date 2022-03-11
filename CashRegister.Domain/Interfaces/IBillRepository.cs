using CashRegister.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Interfaces
{
    public interface IBillRepository
    {
        void Add(Bill bill);
        void Update(Bill bill,ulong id);
        void Delete(Bill bill);
        IEnumerable<Bill> GetAllBills();
        Bill GetBillByID(ulong id);
    }
}
