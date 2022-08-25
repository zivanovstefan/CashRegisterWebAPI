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
        ActionResult<bool> Create(BillVM billVM);
        ActionResult<bool> Update(BillVM billVM);
        ActionResult<bool> Delete(string id);
        ActionResult<BillVM> GetBillByID(string id);
    }
}
