using AutoMapper;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;

namespace RestaurantWebApi.Mapper
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(
                    m => m.Address,
                    c =>
                        c.MapFrom(
                            s =>
                                new Address()
                                {
                                    City = s.City,
                                    PostalCode = s.PostalCode,
                                    Street = s.Street
                                }
                        )
                );
        }
    }
}
