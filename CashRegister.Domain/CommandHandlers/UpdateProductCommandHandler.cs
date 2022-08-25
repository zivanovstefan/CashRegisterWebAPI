using CashRegister.Domain.Commands;
using CashRegister.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetAllProducts().First(x => x.Id == request.Id);
            product.Id = request.Id;
            product.Name = request.Name;
            product.Price = request.Price;
            _productRepository.Update(product, product.Id);
            return Task.FromResult(true);
        }
    }
}
