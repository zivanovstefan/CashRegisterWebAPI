using CashRegister.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface IBillService
    {
        IEnumerable<BillVM> GetAllBills();
        void Create(BillVM billVM);
        void Update(BillVM billVM);
        void Delete(string id);
        BillVM GetBillByID(string id);
    }
}
