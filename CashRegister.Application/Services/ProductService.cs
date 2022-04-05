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
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMediatorHandler bus, IMapper mapper)
        {
            _productRepository = productRepository;
            _bus = bus;
            _mapper = mapper;
         }
        public void Create(ProductVM productVM)
        {
            _bus.SendCommand(_mapper.Map<CreateProductCommand>(productVM));
        }
        public void Update(ProductVM productVM)
        {
            _bus.SendCommand(_mapper.Map<UpdateProductCommand>(productVM));
        }
        public void Delete(int id)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == id);
            _productRepository.Delete(product);
        }

        public IEnumerable<ProductVM> GetAllProducts()
        {
            return _productRepository.GetAllProducts().ProjectTo<ProductVM>(_mapper.ConfigurationProvider);
        }
    }
}
