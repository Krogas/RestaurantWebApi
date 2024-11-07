using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;
using RestaurantWebApi.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantContext _dbContext;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(
            RestaurantContext dbContext,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings
        )
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDto userDto)
        {
            User newUser =
                new()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    EmailAddres = userDto.Email,
                    DateOfBirth = userDto.DateOfBirth,
                    Nationality = userDto.Nationality,
                    RoleId = userDto.RoleId
                };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, userDto.Password);

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string GenerateToken(LoginDto loginDto)
        {
            var user = _dbContext
                .Users.Include(x => x.Role)
                .FirstOrDefault(x => x.EmailAddres == loginDto.Email);
            if (user == null)
                throw new BadRequestException("Invalid email or password");

            if (
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password)
                == PasswordVerificationResult.Failed
            )
                throw new BadRequestException("Invalid email or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(new Claim("Nationality", user.Nationality));
            }

            if (!string.IsNullOrEmpty(user.DateOfBirth.Value.ToString()))
            {
                claims.Add(new Claim("BirthDate", user.DateOfBirth.Value.ToString("yyy-MM-dd")));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey)
            );
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expired = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expired,
                signingCredentials: cred
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
