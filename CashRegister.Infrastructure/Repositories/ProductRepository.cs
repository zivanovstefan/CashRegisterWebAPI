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
        private CashRegisterDBContext _context;
        public ProductRepository(CashRegisterDBContext context)
        {
            _context = context;
        }
        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products;
        }
        public void Add(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product, int id)
        {
            var chosenProduct = GetAllProducts().FirstOrDefault(x => x.Id == id);
            if (chosenProduct != null)
            {
                chosenProduct.Name = product.Name;
                chosenProduct.Price = product.Price;
            }
            _context.SaveChanges();
        }
        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
