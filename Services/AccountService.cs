using Microsoft.AspNetCore.Identity;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;

namespace RestaurantWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(RestaurantContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
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
    }
}
