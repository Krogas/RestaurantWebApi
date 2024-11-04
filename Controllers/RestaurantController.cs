using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;

namespace RestaurantWebApi.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants.Include(x => x.Address)
                .Include(x => x.Dishes)
                .ToList();
            var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restaurantsDto);
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<IEnumerable<RestaurantDto>> Get([FromRoute] int restaurantId)
        {
            var restaurant = _dbContext
                .Restaurants.Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant is null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return Ok(restaurantDto);
        }
    }
}
