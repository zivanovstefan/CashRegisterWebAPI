using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Models;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Context;

namespace CashRegister.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CashRegisterDBContext _context;
        public ProductRepository(CashRegisterDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }
        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
