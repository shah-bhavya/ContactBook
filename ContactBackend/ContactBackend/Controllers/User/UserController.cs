using AutoMapper;
using Contact.API.DTOs.User;
using Contact.Core.Entities.Users;
using Contact.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Contact.API.Controllers.User
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var response = _accountService.Authenticate(dto.Email, dto.Password);
            if (response != null)
            {
                HttpContext.Response.Cookies.Append("AuthToken", response.Token, new CookieOptions() { HttpOnly = true });
                return Ok(new { token = response.Token });
            }
            return BadRequest();
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            var userEntity = _mapper.Map<RegisterDTO, UserEntity>(dto);
            userEntity.UserId = Guid.NewGuid();
            userEntity.CreatedDate = DateTime.Now;
            userEntity.ModifiedDate = DateTime.Now;

            if (!string.IsNullOrEmpty(userEntity.Email) && !string.IsNullOrEmpty(userEntity.Password))
            {
                _accountService.RegisterUser(userEntity);
                _accountService.Save();
                return Ok();
            }

            return BadRequest();
        }
    }
}
