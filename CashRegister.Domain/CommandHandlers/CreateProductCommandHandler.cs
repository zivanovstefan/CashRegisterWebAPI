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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
            };
            _productRepository.Add(product);
            return Task.FromResult(true);
        }
    }
}
