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
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMediatorHandler bus, IMapper mapper)
        {
            _productRepository = productRepository;
            _bus = bus;
            _mapper = mapper;
         }
        public ActionResult<bool> Create(ProductVM productVM)
        {
            if(productVM == null)
            {
                return false;
            }
            _bus.SendCommand(_mapper.Map<CreateProductCommand>(productVM));
            return true;
        }
        public ActionResult<bool> Update(ProductVM productVM)
        {
            if (productVM == null)
            {
                return false;
            }
            _bus.SendCommand(_mapper.Map<UpdateProductCommand>(productVM));
            return true;
        }
        public ActionResult<bool> Delete(int id)
        {
            if (id == 0)
            {
                return false;
            }
            try
            {
                var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == id);
                _productRepository.Delete(product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ProductVM> GetAllProducts()
        {
            var x = _productRepository.GetAllProducts();
                
            var y = x.ProjectTo<ProductVM>(_mapper.ConfigurationProvider);

            return y;
        }
    }
}
