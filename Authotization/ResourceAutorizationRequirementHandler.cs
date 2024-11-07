using Microsoft.AspNetCore.Authorization;
using RestaurantWebApi.Entities;
using System.Security.Claims;

namespace RestaurantWebApi.Authotization
{
    public class ResourceAutorizationRequirementHandler
        : AuthorizationHandler<ResourceAutorizationRequirement, Restaurant>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceAutorizationRequirement requirement,
            Restaurant restaurant
        )
        {
            if (
                requirement.Operation == ResourceOperation.Create
                || requirement.Operation == ResourceOperation.Read
            )
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(d => d.Type == ClaimTypes.NameIdentifier).Value;
            if (restaurant.CreatedById == int.Parse(userId) || restaurant.CreatedById is null)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
