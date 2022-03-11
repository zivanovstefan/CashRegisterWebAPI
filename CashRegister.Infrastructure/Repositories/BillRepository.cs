using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Infrastructure.Repositories
{
    public class BillRepository : IBillRepository
    {
        private CashRegisterDBContext _context;
        public BillRepository(CashRegisterDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Bill> GetAllBills()
        {
            return _context.Bills;
        }
        public void Add(Bill bill)
        {
            _context.Add(bill);
            _context.SaveChanges();
        }
        public void Update(Bill bill, long billNumber)
        {
            var chosenBill = GetAllBills().FirstOrDefault(x => x.BillNumber == billNumber);
            if (chosenBill != null)
            {
                chosenBill.PaymentMethod = bill.PaymentMethod;
                chosenBill.CreditCardNumber = bill.CreditCardNumber;
                chosenBill.TotalPrice = bill.TotalPrice;
            }
            _context.SaveChanges();
        }
        public void Delete(Bill bill)
        {
            _context.Remove(bill);
            _context.SaveChanges();
        }
        public Bill GetBillByID(long billNumber)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.BillNumber == billNumber);
            return bill;
        }
    }
}
