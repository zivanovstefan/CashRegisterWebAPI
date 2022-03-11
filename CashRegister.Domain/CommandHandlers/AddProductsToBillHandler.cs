using CashRegister.Domain.Commands;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.CommandHandlers
{
    public class AddProductsToBillHandler : IRequestHandler<AddProductsCommand, bool>
    {
        private readonly IProductBillRepository _productBillRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBillRepository _billRepository;
        public AddProductsToBillHandler(IProductBillRepository productBillRepository, IProductRepository productRepository, IBillRepository billRepository)
        {
            _productBillRepository = productBillRepository;
            _productRepository = productRepository;
            _billRepository = billRepository;
        }
        public Task<bool> Handle(AddProductsCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(x => x.Id == request.ProductId);
            if (product == null)
            {
                return Task.FromResult(false);
            }
            var productBill = new ProductBill()
            {
                BillNumber = request.BillNumber,
                ProductId = request.ProductId,
                ProductsPrice = request.ProductsPrice
            };
            _productBillRepository.Add(productBill);
            return Task.FromResult(true);
        }
    }
}
