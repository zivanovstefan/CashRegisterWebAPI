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
    public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public Task<bool> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
