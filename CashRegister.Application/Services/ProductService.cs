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
using CashRegister.Application.ErrorModels;
using CashRegister.Domain.Common;

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
                var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.Product_Does_Not_Exist,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return new NotFoundObjectResult(errorResponse);
            }
                _productRepository.Delete(product);
                return true;
        }

        public ActionResult<List<ProductVM>> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts().ToList();
            var result = new List<ProductVM>();
            foreach(var product in products)
            {
                result.Add(_mapper.Map<ProductVM>(product));
            }
            return result;
        }
    }
}
