using CashRegister.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserVM> GetUserByIdAsync(int id);
        Task<UserVM> GetUserByUserName(string username);
        Task<UserVM> CreateUser(UserVM newUser);
        UserVM Authenticate(UserCred userCred);

    }
}
