using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Domain.Models;
using CashRegister.Application.ViewModels;

namespace CashRegister.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<ProductVM, Product>();
        }
    }
}
