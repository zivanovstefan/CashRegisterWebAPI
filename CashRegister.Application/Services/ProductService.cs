using CashRegister.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Core.Bus;
using CashRegister.Domain.Commands;

namespace CashRegister.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _bus;
        public ProductService(IProductRepository productRepository, IMediatorHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }
        public void Create(ProductVM productVM)
        {
            var createProductCommand = new CreateProductCommand(
                productVM.Id,
                productVM.Name,
                productVM.Price,
                productVM.Quantity);
            _bus.SendCommand(createProductCommand);
        }
        public void Delete(int id)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == id);
            _productRepository.Delete(product);
        }
        public IEnumerable<ProductVM> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            var allProducts = new List<ProductVM>();
            foreach (var product in products)
            {
                allProducts.Add(new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return allProducts;
        }
    }
}
