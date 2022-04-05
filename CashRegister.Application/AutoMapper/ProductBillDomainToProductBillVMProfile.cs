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
    public class ProductBillDomainToProductBillVMProfile : Profile
    {
        public ProductBillDomainToProductBillVMProfile()
        {
            CreateMap<ProductBillVM, AddProductsCommand>()
                .ConstructUsing(c => new AddProductsCommand(c.BillNumber, c.ProductId, c.ProductsPrice));
        }
    }
}
