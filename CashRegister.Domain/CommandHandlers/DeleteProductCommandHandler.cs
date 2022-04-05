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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = request;
            _productRepository.Add(product);
            return Task.FromResult(true);
        }
    }
}
