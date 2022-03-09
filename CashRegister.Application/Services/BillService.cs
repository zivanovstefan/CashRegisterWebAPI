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
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IMediatorHandler _bus;
        public BillService(IBillRepository billRepository, IMediatorHandler bus)
        {
            _billRepository = billRepository;
            _bus = bus;
        }
        public IEnumerable<BillVM> GetAllBills()
        {
            var bills = _billRepository.GetAllBills();
            var billList = new List<BillVM>();
            foreach (var bill in bills)
            {
                billList.Add(new BillVM
                {
                    Id = bill.Id,
                    PaymentMethod = bill.PaymentMethod,
                    TotalPrice = bill.TotalPrice,
                    CreditCardNumber = bill.CreditCardNumber
                });
            }
            return billList;
        }
        public void Create(BillVM billVM)
        {
            var createBillCommand = new CreateBillCommand(
                billVM.Id,
                billVM.PaymentMethod,
                billVM.TotalPrice,
                billVM.CreditCardNumber);
                _bus.SendCommand(createBillCommand);
        }
        public void Update(BillVM billVM)
        {
            var updateBillCommand = new UpdateBillCommand(
                billVM.Id,
                billVM.PaymentMethod,
                billVM.TotalPrice,
                billVM.CreditCardNumber);
            _bus.SendCommand(updateBillCommand);
        }
        public void Delete(int id)
        {
            var bill = _billRepository.GetAllBills().FirstOrDefault(x => x.Id == id);
            _billRepository.Delete(bill);
        }
        public BillVM GetBillByID(int id)
        {
            var bill = _billRepository.GetBillByID(id);
            var result = new BillVM
            {
                Id = bill.Id,
                PaymentMethod = bill.PaymentMethod,
                TotalPrice = bill.TotalPrice,
                CreditCardNumber = bill.CreditCardNumber
            };
            return result;

        }
    }
}
