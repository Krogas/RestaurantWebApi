using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;
using RestaurantWebApi.Exceptions;

namespace RestaurantWebApi.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto createDishDto)
        {
            GetRestaurant(restaurantId);

            var dishEntity = _mapper.Map<Dish>(createDishDto);
            dishEntity.RestaurantId = restaurantId;
            _dbContext.Dishes.Add(dishEntity);
            _dbContext.SaveChanges();
            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            GetRestaurant(restaurantId);
            var dish = _dbContext.Dishes.FirstOrDefault(
                x => x.Id == dishId && x.RestaurantId == restaurantId
            );
            if (dish == null)
                throw new NotFoundException("Dish not found");

            return _mapper.Map<DishDto>(dish);
        }

        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantWithDishes(restaurantId);

            return _mapper.Map<List<DishDto>>(restaurant.Dishes);
        }

        public void DeleteAll(int restaurantId)
        {
            var restaurant = GetRestaurantWithDishes(restaurantId);

            _dbContext.Dishes.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int restaurantId, int dishId)
        {
            GetRestaurant(restaurantId);
            var dish = _dbContext.Dishes.FirstOrDefault(
                x => x.Id == dishId && x.RestaurantId == restaurantId
            );
            if (dish == null)
                throw new NotFoundException("Dish not found");

            _dbContext.Dishes.Remove(dish);
            _dbContext.SaveChanges();
        }

        private Restaurant GetRestaurant(int restaurantId)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");
            return restaurant;
        }

        private Restaurant GetRestaurantWithDishes(int restaurantId)
        {
            var restaurant = _dbContext
                .Restaurants.Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");
            return restaurant;
        }
    }
}
