using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Domain.Models;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;

namespace CashRegister.Application.AutoMapper
{
    public class ProductVMToDomainProfile : Profile
    {
        public ProductVMToDomainProfile()
        {
            CreateMap<ProductVM, CreateProductCommand>()
                .ConstructUsing(c => new CreateProductCommand(c.Id, c.Name, c.Price));
        }
    }
}
