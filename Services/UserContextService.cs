using System.Security.Claims;

namespace RestaurantWebApi.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? GetUserId =>
            User is null
                ? null
                : int.Parse(User.FindFirst(d => d.Type == ClaimTypes.NameIdentifier).Value);
    }
}
