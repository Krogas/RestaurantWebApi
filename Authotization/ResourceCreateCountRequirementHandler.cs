using Microsoft.AspNetCore.Authorization;
using RestaurantWebApi.Entities;
using System.Security.Claims;

namespace RestaurantWebApi.Authotization
{
    public class ResourceCreateCountRequirementHandler
        : AuthorizationHandler<ResourceCreateCountRequirement>
    {
        private readonly RestaurantContext _restaurantContext;

        public ResourceCreateCountRequirementHandler(RestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceCreateCountRequirement requirement
        )
        {
            var userId = int.Parse(
                context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value
            );

            if (_restaurantContext.Restaurants.Count(x => x.CreatedById == userId) >= 2)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
