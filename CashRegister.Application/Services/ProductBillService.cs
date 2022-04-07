using AutoMapper;
using CashRegister.Application.ErrorModels;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Common;
using CashRegister.Domain.Core.Bus;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Services
{
    public class ProductBillService : IProductBillService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBillRepository _billRepository;
        private readonly IProductBillRepository _productBillRepository;
        private readonly IMapper _mapper;
        public ProductBillService(IProductBillRepository productBillRepository,IMapper mapper, IBillRepository billRepository, IProductRepository productRepository)
        {
            _productBillRepository = productBillRepository;
            _productRepository = productRepository;
            _billRepository = billRepository;
            _mapper = mapper;
        }
        public ICollection<ProductBillVM> GetAllBillProducts()
        {
            var billProducts = _productBillRepository.GetProductBills();
            var result = new List<ProductBillVM>();
            if(billProducts != null)
            {
                foreach (var productBill in billProducts)
                {
                    result.Add(_mapper.Map<ProductBillVM>(productBill));
                }
            }
            return result;
        }
        public void AddProductToBill(ProductBillVM productBillVM)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == productBillVM.ProductId);

            var productBillFromDb = _productBillRepository.GetProductBills()
                .FirstOrDefault(x => x.BillNumber == productBillVM.BillNumber && x.ProductId == productBillVM.ProductId);
            if (productBillFromDb != null)
            {
                int productBillCount = productBillFromDb.ProductQuantity + productBillVM.ProductQuantity;
                productBillFromDb.BillNumber = productBillVM.BillNumber;
                productBillFromDb.ProductId = productBillVM.ProductId;
                productBillFromDb.ProductQuantity = productBillCount;
                productBillFromDb.ProductsPrice = (product.Price * productBillCount);

                var bill = _billRepository.GetBillByID(productBillFromDb.BillNumber);

                _productBillRepository.Update(productBillFromDb);
                _billRepository.AddToTotalPrice((productBillVM.ProductQuantity * product.Price), productBillFromDb.BillNumber);
            }

            productBillVM.ProductsPrice = product.Price * productBillVM.ProductQuantity;

            var productBill = _mapper.Map<ProductBill>(productBillVM);

            var billFromDb = _billRepository.GetBillByID(productBillVM.BillNumber);

                _billRepository.AddToTotalPrice(productBill.ProductsPrice, productBill.BillNumber);
                _productBillRepository.Add(productBill);
        }
        public void Delete(string billNumber, int productId)
        {
            var productBill = _productBillRepository.GetProductBills().FirstOrDefault(x => x.BillNumber == billNumber && x.ProductId == productId);
            _productBillRepository.Delete(productBill);
        }
    }
}
