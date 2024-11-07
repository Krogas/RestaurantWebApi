﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApi.Authotization;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;
using RestaurantWebApi.Exceptions;
using System.Security.Claims;

namespace RestaurantWebApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationSerivce;

        public RestaurantService(
            RestaurantContext dbContext,
            IMapper mapper,
            ILogger<RestaurantService> logger,
            IAuthorizationService authorizationService
        )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationSerivce = authorizationService;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants.Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
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

        public int Create(CreateRestaurantDto createRestaurantDto, int userId)
        {
            var restaurant = _mapper.Map<Restaurant>(createRestaurantDto);
            restaurant.CreatedById = userId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            _logger.LogWarning($"DELETE action on Restaurant IDENTITY: {id} invoked");

            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                _logger.LogError(
                    $"DELETE action on Restaurant IDENTITY: {id} invoked, but not passed no Restaurant available for that ID"
                );
                throw new NotFoundException("Restaurant not found");
            }
            var authorizationResult = _authorizationSerivce
                .AuthorizeAsync(
                    user,
                    restaurant,
                    new ResourceAutorizationRequirement(ResourceOperation.Delete)
                )
                .Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Restaurant created by different user");

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationSerivce
                .AuthorizeAsync(
                    user,
                    restaurant,
                    new ResourceAutorizationRequirement(ResourceOperation.Update)
                )
                .Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Restaurant created by different user");
            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}
