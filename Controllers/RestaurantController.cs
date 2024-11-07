using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Services;
using System.Security.Claims;

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
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            return Ok(_restaurantService.GetAll());
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
                $"api/restaurant/{_restaurantService.Create(createRestaurantDto, int.Parse(User.FindFirst(d => d.Type == ClaimTypes.NameIdentifier).Value))}",
                null
            );
        }

        [HttpDelete("{restaurantId}")]
        public ActionResult DeleteRestaurant([FromRoute] int restaurantId)
        {
            _restaurantService.Delete(restaurantId, User);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(id, dto, User);

            return Ok();
        }
    }
}
