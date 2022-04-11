using AutoMapper;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.AutoMapper
{
    public class BillDomainToVMProfile : Profile
    {
        public BillDomainToVMProfile()
        {
            CreateMap<Bill, BillVM>();
            CreateMap<ProductBill, ProductBillVM>();
            CreateMap<ProductVM, CreateBillCommand>();
        }
    }
}
