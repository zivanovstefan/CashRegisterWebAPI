using CashRegister.Domain.Commands;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Domain.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = request.Id,
                Username = request.Username,
                Password = request.Password,
                Role = request.Role,

            };
            _userRepository.Insert(user);
            return Task.FromResult(true);
        }
    }
}
