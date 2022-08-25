using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Models;

namespace CashRegister.Application.AutoMapper
{
    public class ProductDomainToVMProfile : Profile
    {
        public ProductDomainToVMProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, CreateProductCommand>();
        }
    }
}
