using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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
            //var bills = _context.Bills;
            //return bills;
            var bills = _context.Bills.Include(x => x.BillProducts);
            return bills;
        }
        public void Add(Bill bill)
        {
            _context.Add(bill);
            _context.SaveChanges();
        }
        public void Update(Bill bill, string billNumber)
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
        public void AddToTotalPrice(int TotalPrice, string id)
        {
            var bill = GetAllBills().FirstOrDefault(x => x.BillNumber == id);

            if (bill != null)
            {
                if (bill.TotalPrice == null)
                {
                    bill.TotalPrice = 0;
                }
                bill.TotalPrice = bill.TotalPrice + TotalPrice;
            }
            _context.SaveChanges();
        }
        public void RemoveFromTotalPrice(int TotalPrice, string id)
        {
            var bill = GetAllBills().FirstOrDefault(x => x.BillNumber == id);

            if (bill != null)
            {
                bill.TotalPrice = bill.TotalPrice - TotalPrice;
            }
            _context.SaveChanges();
        }
        public Bill GetBillByID(string billNumber)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.BillNumber == billNumber);
            return bill;
        }
    }
}
