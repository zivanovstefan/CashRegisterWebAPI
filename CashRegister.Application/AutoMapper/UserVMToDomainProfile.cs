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
    public class UserVMToDomainProfile : Profile
    {
        public UserVMToDomainProfile()
        {
            CreateMap<UserVM, CreateUserCommand>()
                .ConstructUsing(c => new CreateUserCommand(c.Id, c.Username, c.Password, c.Role));
        }
    }
}
