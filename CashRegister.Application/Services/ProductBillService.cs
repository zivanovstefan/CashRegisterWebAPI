using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Core.Bus;
using CashRegister.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Services
{
    public class ProductBillService : IProductBillService
    {
        private readonly IProductBillRepository _productBillRepository;
        private readonly IMediatorHandler _bus;
        public ProductBillService(IProductBillRepository productBillRepository, IMediatorHandler bus)
        {
            _productBillRepository = productBillRepository;
            _bus = bus;
        }
        public ICollection<ProductBillVM> GetAllBillProducts()
        {
            var billProducts = _productBillRepository.GetProductBills();
            var result = new List<ProductBillVM>();
            if(billProducts != null)
            {
                foreach (var billProduct in billProducts)
                {
                    result.Add(new ProductBillVM
                        {
                        BillNumber = billProduct.BillNumber,
                        ProductId = billProduct.ProductId,
                        ProductQuantity = billProduct.ProductQuantity,
                        ProductsPrice = billProduct.ProductsPrice
                    });
                }
            }
            return result;
        }
        public void AddProductToBill(ProductBillVM productBillVM)
        {
            var addProductsCommand = new AddProductsCommand (
                productBillVM.BillNumber,
                productBillVM.ProductId,
                productBillVM.ProductsPrice);
            _bus.SendCommand(addProductsCommand);
        }
        public void Delete(ulong id1, int id2)
        {
            var productBill = _productBillRepository.GetProductBills().FirstOrDefault(x =>x.BillNumber == id1 && x.ProductId ==id2);
        }
    }
}
