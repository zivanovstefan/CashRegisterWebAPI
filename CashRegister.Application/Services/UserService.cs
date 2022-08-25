using AutoMapper;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserVM> GetUserByIdAsync(int id)
        {
            var data = await _userRepository.GetByIdAsync(id);

            if (data == null)
            {
                return null;
            }

            UserVM viewModel = new UserVM
            {
                Id = data.Id,
                Username = data.Username,
                Password = data.Password,
                Role = data.Role,
            };

            return viewModel;
        }

        public async Task<UserVM> GetUserByUserName(string username)
        {
            var data = _userRepository.GetByUserName(username);

            if (data == null)
            {
                return null;
            }

            UserVM viewModel = new UserVM
            {
                Id = data.Id,
                Username = data.Username,
                Password = data.Password,
                Role = data.Role,
            };

            return viewModel;
        }
        public async Task<UserVM> CreateUser(UserVM newUser)
        {
            User userToCreate = new User()
            {
                Username = newUser.Username,
                Password = newUser.Password,
                Role = newUser.Role,
            };

            var data = _userRepository.Insert(userToCreate);
            if (data == null)
            {
                return null;
            }
            _userRepository.Save();

            UserVM viewModel = new UserVM()
            {
                Id = data.Id,
                Username=data.Username,
                Password=data.Password,
                Role = data.Role
            };
            return viewModel;
        }
        public UserVM Authenticate(UserCred userCred)
        {
            var currentUser = _userRepository.GetUserByCredentials(userCred.Username, userCred.Password);
            if (currentUser != null)
            {
                var resultUser = new UserVM()
                {
                    Username=currentUser.Username,
                    Password=currentUser.Password,
                    Role = currentUser.Role
                };
                return resultUser;
            }
            return null;
        }
    }
}
