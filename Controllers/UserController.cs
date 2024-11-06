using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebApi.Dto;
using RestaurantWebApi.Entities;
using RestaurantWebApi.Services;

namespace RestaurantWebApi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(IAccountService service, IPasswordHasher<User> passwordHasher)
        {
            _service = service;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            _service.RegisterUser(registerUserDto);
            return Created();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            string token = _service.GenerateToken(loginDto);
            return Ok(token);
        }
    }
}
