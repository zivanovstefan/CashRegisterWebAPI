using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.Domain.CommandHandlers
{
    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, bool>
    {
        private readonly IBillRepository _billRepository;
        public CreateBillCommandHandler(IBillRepository billrepository)
        {
            _billRepository = billrepository;
        }
        public Task<bool> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var bill = new Bill()
            {
                Id = request.Id,
                PaymentMethod = request.PaymentMethod,
                TotalPrice = request.TotalPrice,
                CreditCardNumber = request.CreditCardNumber
            };
            _billRepository.Add(bill);
            return Task.FromResult(true);
        }
    }
}
