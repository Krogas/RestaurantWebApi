using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Services;

namespace RestaurantWebApi.Controllers
{
    [ApiController]
    [Route("/api/restaurant/{restaurantId}/dish")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dishDto)
        {
            return Created(
                $"/api/restaurant/{restaurantId}/dish/{_service.Create(restaurantId, dishDto)}",
                null
            );
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            return Ok(_service.GetById(restaurantId, dishId));
        }

        [HttpGet]
        [Authorize(Policy = "AtLeast20")]
        public ActionResult<List<DishDto>> Get([FromRoute] int restaurantId)
        {
            return Ok(_service.GetAll(restaurantId));
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _service.DeleteAll(restaurantId);
            return Ok();
        }

        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _service.DeleteById(restaurantId, dishId);
            return Ok();
        }
    }
}
