using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private CashRegisterDBContext _context;

        public UserRepository(CashRegisterDBContext context)
        {
            _context = context;
        }

        public User Delete(object id)
        {
            User existing = _context.Users.Find(id);
            var result = _context.Users.Remove(existing).Entity;

            return result;
        }

        public async Task<List<User>> GetAll()
        {
            var data = await _context.Users.ToListAsync();

            return data;
        }

        public async Task<User> GetByIdAsync(object id)
        {
            return await _context.Users.FindAsync(id);
        }

        public User GetByUserName(string username)
        {
            var data = _context.Users.SingleOrDefault(x => x.Username == username);

            return data;
        }

        public User Insert(User obj)
        {
            return _context.Users.Add(obj).Entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public User Update(User obj)
        {
            var updatedEntry = _context.Users.Attach(obj).Entity;
            _context.Entry(obj).State = EntityState.Modified;

            return updatedEntry;
        }

        public User GetUserByCredentials(string username, string password)
        {
            var data = _context.Users.SingleOrDefault(o => o.Username.ToLower() == username && o.Password == password);
            return data;
        }
    }
}
