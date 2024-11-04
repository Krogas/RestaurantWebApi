using Microsoft.AspNetCore.Mvc;

namespace RestaurantWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForeCastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForeCastService service
        )
        {
            _logger = logger;
            _service = service;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get([FromQuery] int count, [FromBody] TemperatureRequest request)
        //{
        //    var reasult = _service.Get(int count, TemperatureRequest request);
        //    return reasult;
        //}

        //[HttpGet("currentDay/{max}")]
        ////[Route("currentDay")]
        //public IEnumerable<WeatherForecast> Get2([FromQuery] int take, [FromRoute] int max)
        //{
        //    var reasult = _service.Get();
        //    return reasult;
        //}

        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {
            //HttpContext.Response.StatusCode = 404;
            return StatusCode(404, $"Hello {name}");
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Get(
            [FromQuery] int count,
            [FromBody] TemperatureRequest request
        )
        {
            if (count < 0 || request.maxTemperature < request.minTemperature)
            {
                return BadRequest();
            }
            var reasult = _service.Get(count, request.minTemperature, request.maxTemperature);
            return Ok(reasult);
        }
    }
}
