using RestaurantWebApi.Dto;
using System.Security.Claims;

namespace RestaurantWebApi.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto createRestaurantDto, int userId);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id, ClaimsPrincipal user);
        void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
    }
}
