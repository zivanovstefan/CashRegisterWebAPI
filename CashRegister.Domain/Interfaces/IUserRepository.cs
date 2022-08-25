using CashRegister.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetByUserName(string username);
        User GetUserByCredentials(string username, string password);
        Task<User> GetByIdAsync(object id);
        User Insert(User obj);
        void Save();
    }
}
