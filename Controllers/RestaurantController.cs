using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Services;

namespace RestaurantWebApi.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [Authorize(Policy = "HasNationality")]
        [Authorize(Policy = "CreatedAtLeast2Restaurants")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll(
            [FromQuery] RestaurantQuery restaurantQuery
        )
        {
            return Ok(_restaurantService.GetAll(restaurantQuery));
        }

        [HttpGet("{restaurantId}")]
        public ActionResult<IEnumerable<RestaurantDto>> Get([FromRoute] int restaurantId)
        {
            var restaurant = _restaurantService.GetById(restaurantId);

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
        {
            return Created(
                $"api/restaurant/{_restaurantService.Create(createRestaurantDto)}",
                null
            );
        }

        [HttpDelete("{restaurantId}")]
        public ActionResult DeleteRestaurant([FromRoute] int restaurantId)
        {
            _restaurantService.Delete(restaurantId);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(id, dto);

            return Ok();
        }
    }
}
