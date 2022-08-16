using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CashRegister.Application.Interfaces;
using CashRegister.Application.ViewModels;
using CashRegister.Domain.Common;
using Microsoft.EntityFrameworkCore;
using CashRegister.Application.ErrorModels;
using System.Text;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserVM>> GetbyIdAsync(int id)
        {
            UserVM model;

            model = await _userService.GetUserByIdAsync(id);

            if (model == null)
            {
                return NotFound(Messages.User_Not_Found);
            }

            return Ok(model);
        }

        // <summary>
        /// Gets User by UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("byusername/{username}")]
        public async Task<ActionResult<UserVM>> GetbyUserNameAsync(string username)
        {
            UserVM model;

            model = await _userService.GetUserByUserName(username);

            if (model == null)
            {
                return NotFound(Messages.User_Not_Found);
            }

            return Ok(model);
        }
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserVM userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserVM domainModel = new UserVM()
            {
                Username = userModel.Username,
                Password = userModel.Password,
                Role = userModel.Role
            };

            UserVM createUser;

            try
            {
                createUser = await _userService.CreateUser(domainModel);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            if (createUser == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.User_Creation_Error,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }

            var token = Generate(createUser);
            return Ok(token);
        }

        [HttpPost("LogIn")]
        [AllowAnonymous]
        public IActionResult LogIn([FromBody] UserCred userCred)
        {
            var user = Authenticate(userCred);
            if (user == null)
            {
                return NotFound("User with inserted credentials does not exist");
            }
            var token = Generate(user);
            return Ok(token);
        }

        private string Generate(UserVM user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretKey!753159"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:3000",
                audience: "http://localhost:3000",
              claims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserVM Authenticate(UserCred userCred)
        {
            var currentUser = _userService.Authenticate(userCred);
            return currentUser;
        }
    }
}
