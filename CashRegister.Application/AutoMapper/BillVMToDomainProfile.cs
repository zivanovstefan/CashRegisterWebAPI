using AutoMapper;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.AutoMapper
{
    public class BillVMToDomainProfile : Profile
    {
        public BillVMToDomainProfile()
        {
            CreateMap<BillVM, CreateBillCommand>()
                .ConstructUsing(c => new CreateBillCommand(c.BillNumber, c.PaymentMethod, c.TotalPrice, c.CreditCardNumber));
            CreateMap<BillVM, UpdateBillCommand>()
                .ConstructUsing(c => new UpdateBillCommand(c.BillNumber, c.PaymentMethod, c.CreditCardNumber));
        }
    }
}
