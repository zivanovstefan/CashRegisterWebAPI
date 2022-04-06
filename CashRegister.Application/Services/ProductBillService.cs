using AutoMapper;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Core.Bus;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
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
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        public ProductBillService(IProductBillRepository productBillRepository, IMediatorHandler bus, IMapper mapper)
        {
            _productBillRepository = productBillRepository;
            _bus = bus;
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
            //var addProductsCommand = new AddProductsCommand (
            //    productBillVM.BillNumber,
            //    productBillVM.ProductId,
            //    productBillVM.ProductsPrice);
            //_bus.SendCommand(addProductsCommand);
            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == productBillVM.ProductId);

            var billproductfromDB = _productBillRepository.GetProductBills()
                .FirstOrDefault(x => x.BillNumber == productBillVM.BillNumber && x.ProductId == productBillVM.ProductId);
            if (billproductfromDB != null)
            {
                int productBillCount = billproductfromDB.ProductQuantity + productBillVM.ProductQuantity;
                billproductfromDB.BillNumber = productBillVM.BillNumber;
                billproductfromDB.ProductId = productBillVM.ProductId;
                billproductfromDB.ProductQuantity = productBillCount;
                billproductfromDB.ProductsPrice = (product.Price * productBillCount);


                var bill = _billRepository.GetBillByID(billproductfromDB.BillNumber);

                _productBillRepository.Update(billproductfromDB);
                _billRepository.AddToTotalPrice((productBillVM.ProductQuantity * product.Price), billproductfromDB.BillNumber);
            }

            productBillVM.ProductsPrice = product.Price * productBillVM.ProductQuantity;

            var productBill = _mapper.Map<ProductBill>(productBillVM);

            var billFromDb = _billRepository.GetBillByID(productBillVM.BillNumber);

                _billRepository.AddToTotalPrice(productBill.ProductsPrice, productBill.BillNumber);
                _productBillRepository.Add(productBill);
        }
        public void Delete(string id1, int id2)
        {
            var productBill = _productBillRepository.GetProductBills().FirstOrDefault(x =>x.BillNumber == id1 && x.ProductId ==id2);
        }
    }
}
