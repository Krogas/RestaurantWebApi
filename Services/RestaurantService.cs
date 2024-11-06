﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;
using RestaurantWebApi.Exceptions;

namespace RestaurantWebApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(
            RestaurantContext dbContext,
            IMapper mapper,
            ILogger<RestaurantService> logger
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants.Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundExceptions("Restaurant not found");
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants.Include(x => x.Address)
                .Include(x => x.Dishes)
                .ToList();

            var restaurantDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantDto;
        }

        public int Create(CreateRestaurantDto createRestaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogWarning($"DELETE action on Restaurant IDENTITY: {id} invoked");

            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                _logger.LogError(
                    $"DELETE action on Restaurant IDENTITY: {id} invoked, but not passed no Restaurant available for that ID"
                );
                throw new NotFoundExceptions("Restaurant not found");
            }
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
                throw new NotFoundExceptions("Restaurant not found");

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}
