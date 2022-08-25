using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CashRegister.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new ProductVMToDomainProfile());
                configuration.AddProfile(new ProductDomainToVMProfile());
            });
        }
    }
}
