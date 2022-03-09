using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Interfaces;
using MediatR;

namespace CashRegister.Domain.CommandHandlers
{
    public class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand, bool>
    {
        private readonly IBillRepository _billRepository;
        public UpdateBillCommandHandler(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public Task<bool> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            var bill = _billRepository.GetAllBills().First(x => x.Id == request.Id);
            bill.Id = request.Id;
            bill.PaymentMethod = request.PaymentMethod;
            bill.TotalPrice = request.TotalPrice;
            bill.CreditCardNumber = request.CreditCardNumber;
            _billRepository.Update(bill, bill.Id);
            return Task.FromResult(true);
        }
    }
}
