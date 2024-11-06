using RestaurantWebApi.Dto;

namespace RestaurantWebApi.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto userDto);
    }
}