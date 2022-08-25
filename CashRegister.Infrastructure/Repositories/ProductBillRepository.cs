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
    public class ProductBillRepository : IProductBillRepository
    {
        private CashRegisterDBContext _context;
        public ProductBillRepository(CashRegisterDBContext context)
        {
            _context = context;
        }
        public void Add(ProductBill productBill)
        {
            _context.Add(productBill);
            _context.SaveChanges();
        }
        public void Delete(ProductBill productBill)
        {
            _context.Remove(productBill);
            _context.SaveChanges();
        }
        public void Update(ProductBill productBill)
        {
            _context.Update(productBill);
            _context.SaveChanges();
        }
        public IEnumerable<ProductBill> GetProductBills()
        {
            return _context.BillProducts;
        }
    }
}
