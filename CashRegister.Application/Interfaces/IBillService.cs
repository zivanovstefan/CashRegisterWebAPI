﻿using CashRegister.Application.ViewModels;
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
        void Delete(int id);
        BillVM GetBillByID(int id);
    }
}