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
        private readonly IMediatorHandler _bus;
        public ProductBillService(IProductBillRepository productBillRepository,IMapper mapper, IBillRepository billRepository, IProductRepository productRepository, IMediatorHandler bus)
        {
            _productBillRepository = productBillRepository;
            _productRepository = productRepository;
            _billRepository = billRepository;
            _mapper = mapper;
            _bus = bus;
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
                int newProductQuantity = productBillFromDb.ProductQuantity + productBillVM.ProductQuantity;
                productBillFromDb.BillNumber = productBillVM.BillNumber;
                productBillFromDb.ProductId = productBillVM.ProductId;
                productBillFromDb.ProductQuantity = newProductQuantity;
                productBillFromDb.ProductsPrice = (product.Price * newProductQuantity);

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
        public ActionResult<bool> DeleteProductsFromBill(string BillNumber, int ProductId, int quantity)
        {
            //conversion to list
            List<ProductBill> productBills = _productBillRepository.GetProductBills().ToList();
            var productBillsList = productBills.FirstOrDefault(x => x.BillNumber == BillNumber && x.ProductId == ProductId);
            if (productBillsList == null)
            {
                return false;
            }
            if (quantity == productBillsList.ProductQuantity)
            {
                _billRepository.RemoveFromTotalPrice(productBillsList.ProductsPrice, productBillsList.BillNumber);
                _productBillRepository.Delete(productBillsList);
            }
            else if (quantity > productBillsList.ProductQuantity)
            {
                return false;
            }
            else
            {
                var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == productBillsList.ProductId);
                if (product == null)
                {
                    return false;
                }
                var NewProductQuantity = productBillsList.ProductQuantity - quantity;
                productBillsList.ProductQuantity = NewProductQuantity;
                productBillsList.ProductsPrice = (product.Price * NewProductQuantity);
                _productBillRepository.Update(productBillsList);
                _billRepository.RemoveFromTotalPrice((product.Price * quantity), productBillsList.BillNumber);
            }
            return true;
        }
    }
}
