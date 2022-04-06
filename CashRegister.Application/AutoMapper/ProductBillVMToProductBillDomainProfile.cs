using AutoMapper;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.AutoMapper
{
    public class ProductBillVMToProductBillDomainProfile : Profile
    {
        public ProductBillVMToProductBillDomainProfile()
        {
            CreateMap<ProductBillVM, ProductBill>();
        }
    }
}
