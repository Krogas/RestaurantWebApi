﻿using RestaurantWebApi.Dto;

namespace RestaurantWebApi.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto createRestaurantDto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto? GetById(int id);
        bool Delete(int id);
        bool Update(int id, UpdateRestaurantDto dto);
    }
}