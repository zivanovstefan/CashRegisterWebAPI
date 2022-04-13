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
        public ActionResult<List<ProductBillVM>> GetAllBillProducts()
        {
            var billProducts = _productBillRepository.GetProductBills();
            var result = new List<ProductBillVM>();
            if(billProducts.Count() == 0)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.Product_Does_Not_Exist,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return new BadRequestObjectResult(errorResponse);
            }
            foreach (var productBill in billProducts)
            {
                result.Add(_mapper.Map<ProductBillVM>(productBill));
            }
            return result;
        }
        public ActionResult<bool> AddProductToBill(ProductBillVM productBillVM)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == productBillVM.ProductId);
            if (product == null)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.Product_Does_Not_Exist,
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return new NotFoundObjectResult(errorResponse);
            }
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
                if (bill.TotalPrice + productBillFromDb.ProductsPrice > 5000)
                {
                    var errorResponse = new ErrorResponseModel()
                    {
                        ErrorMessage = Messages.BillTotalPriceTooHigh,
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };
                    return new BadRequestObjectResult(errorResponse);
                }
                else
                {
                    _productBillRepository.Update(productBillFromDb);
                    _billRepository.AddToTotalPrice((productBillVM.ProductQuantity * product.Price), productBillFromDb.BillNumber);
                    return true;
                }
            }
            productBillVM.ProductsPrice = product.Price * productBillVM.ProductQuantity;

            var productBill = _mapper.Map<ProductBill>(productBillVM);

            var billFromDb = _billRepository.GetBillByID(productBillVM.BillNumber);
            if (billFromDb.TotalPrice + productBill.ProductsPrice > 5000)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.BillTotalPriceTooHigh,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
                return new BadRequestObjectResult(errorResponse);
            }
            _billRepository.AddToTotalPrice(productBill.ProductsPrice, productBill.BillNumber);
                _productBillRepository.Add(productBill);
            return true;
        }
        public ActionResult<bool> DeleteProductsFromBill(string BillNumber, int ProductId, int quantity)
        {
            //conversion to list
            List<ProductBill> productBills = _productBillRepository.GetProductBills().ToList();
            var productBillToUpdate = productBills.FirstOrDefault(x => x.BillNumber == BillNumber && x.ProductId == ProductId);
            if (productBillToUpdate == null)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage= Messages.BillProductErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return new NotFoundObjectResult(errorResponse);
            }
            if (quantity == productBillToUpdate.ProductQuantity)
            {
                _billRepository.RemoveFromTotalPrice(productBillToUpdate.ProductsPrice, productBillToUpdate.BillNumber);
                _productBillRepository.Delete(productBillToUpdate);
            }
            else if (quantity > productBillToUpdate.ProductQuantity)
            {
                var errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = Messages.QuantityTooHigh,
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
                return new BadRequestObjectResult(errorResponse);
            }
            else
            {
                var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == productBillToUpdate.ProductId);
                if (product == null)
                {
                    return false;
                }
                var NewProductQuantity = productBillToUpdate.ProductQuantity - quantity;
                productBillToUpdate.ProductQuantity = NewProductQuantity;
                productBillToUpdate.ProductsPrice = (product.Price * NewProductQuantity);
                _productBillRepository.Update(productBillToUpdate);
                _billRepository.RemoveFromTotalPrice((product.Price * quantity), productBillToUpdate.BillNumber);
            }
            return true;
        }
    }
}
