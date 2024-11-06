using RestaurantWebApi.Dto;

namespace RestaurantWebApi.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto createDishDto);
        DishDto GetById(int restaurantId, int dishId);
        List<DishDto> GetAll(int restaurantId);
        void DeleteAll(int restaurantId);
        void DeleteById(int restaurantId, int dishId);
    }
}
