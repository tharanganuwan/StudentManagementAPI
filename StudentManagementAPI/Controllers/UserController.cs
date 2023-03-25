using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using StudentManagementAPI.Services.users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudentManagementAPI.Helpers;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace StudentManagementAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository service, IMapper mapper, ILogger<UserController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Register")]
        public ActionResult<UserDto> CreateUser(CreateUserDto newUser) 
        {
            try
            {
                var user = _service.GetUserFromEmail(newUser.Email);
                if (user != null)
                {
                    _logger.LogInformation($"This email : {user.Email} alrady use please use another one");
                    return StatusCode(500, "This email alrady use please use another one");
                }
                var createUser = _mapper.Map<User>(newUser);
                createUser = _service.CreateUser(createUser);
                var returnUser = _mapper.Map<UserDto>(createUser);
                _logger.LogInformation($"Save userId: {createUser.Id} in CreateUser Method");
                return Ok(returnUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Save New user : {ex.Message}");
                return StatusCode(500, $"An error occurred while Save New user : {ex.Message}");
            }
            
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateUser(int id,UpdateUserDto user)
        {
            try
            {
                var cUser = _service.GetUser(id);
                if (cUser is null)
                {
                    _logger.LogInformation("No user found in UpdateUser method");
                    return NotFound();
                }
                var updateUser = _mapper.Map<User>(user);
                updateUser.Id = id;
                _service.UpdateUser(updateUser);
                _logger.LogInformation($"Updated user : {updateUser.Id} in UpdateUser method");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Updating users in UpdateUser method : {ex.Message}");
                return StatusCode(500, $"An error occurred while Updating users : {ex.Message}");
            }
            
        }

        [HttpGet("GetUsers")]
        public ActionResult<UserDto> GetAllUsers() 
        {
            try
            {
                List<User> users = _service.GetAllUser();
                if (users is null)
                {
                    _logger.LogInformation("No users found in GetAllUsers method");
                    return NotFound();
                }
                _logger.LogInformation($"Retrieved {users.Count} users in GetAllUsers method");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving users in GetAllUsers method : {ex.Message}");
                return StatusCode(500, $"An error occurred while retrieving users: {ex.Message}");
            }
            
        }

        [HttpPost("Login")]
        public IActionResult GetUser(UserLoginDto details)
        {
            try
            {
                var user = _service.Authenticate(details);
                if (user is null)
                {
                    _logger.LogInformation($"Unauthorized in Authenticate method");
                    return Unauthorized();
                }

                // create JWT token
                var keyBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                var key = new SymmetricSecurityKey(keyBytes);
                var tokenHandler = new JwtSecurityTokenHandler();
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };

                var token = new JwtSecurityToken(
                         Constants.Audience,
                         Constants.Issure,
                         claims,
                         notBefore: DateTime.Now,
                         expires: DateTime.Now.AddHours(1),
                         signingCredentials);
                var tokenString = tokenHandler.WriteToken(token);
                _logger.LogInformation($"Created  accessToken in Authenticate method");
                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Creating Token: {ex.Message}");
                return StatusCode(500, $"An error occurred while Creating Token: {ex.Message}");
            }
            

        }
      

    }
}
