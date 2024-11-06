using RestaurantWebApi.Dto;

namespace RestaurantWebApi.Services
{
    public interface IAccountService
    {
        string GenerateToken(LoginDto loginDto);
        void RegisterUser(RegisterUserDto userDto);
    }
}
