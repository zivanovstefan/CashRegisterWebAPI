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
        void Update(Bill bill,string id);
        void Delete(Bill bill);
        void AddToTotalPrice(int TotalPrice, string id);
        void RemoveFromTotalPrice(int TotalPrice, string id);
        IQueryable<Bill> GetAllBills();
        Bill GetBillByID(string id);
    }
}
