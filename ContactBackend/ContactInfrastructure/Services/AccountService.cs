using Contact.Core.Entities.Common;
using Contact.Core.Interfaces.Auth;
using Contact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contact.Core.Entities.Users;

namespace Contact.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration iconfiguration;
        public AccountService(IConfiguration iconfiguration, AppDbContext context)
        {
            this.iconfiguration = iconfiguration;
            this._context = context;
        }
        public AuthenticationResponse Authenticate(string email, string password)
        {
            var userObj = _context.Users.Where(x => x.Email == email.Trim() && x.Password == password).FirstOrDefault();

            if (userObj == null)
                return null;

            //Dictionary<string, string> claims = new Dictionary<string, string>();
            //claims.Add("id", userResponse.Data.id.ToString());

            //Generate Token
            var token = CreateToken(userObj.UserId.ToString(), userObj.Email);
            return new AuthenticationResponse
            {
                Email = email,
                IsSuccess = true,
                Token = token
            };
        }

        public void RegisterUser(UserEntity entity)
        {
            _context.Users.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private string CreateToken(string userId, string email, Dictionary<string, string> additionalClaims = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("UserId is null");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email is null");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                      new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(iconfiguration["JWT:ExpirationTime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
