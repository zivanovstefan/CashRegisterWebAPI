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
        public void Add(Product products)
        {
            _context.Products.Add(products);
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
        }
    }
}
