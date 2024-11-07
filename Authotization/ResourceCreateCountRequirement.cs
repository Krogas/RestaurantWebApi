using Microsoft.AspNetCore.Authorization;

namespace RestaurantWebApi.Authotization
{
    public class ResourceCreateCountRequirement : IAuthorizationRequirement
    {
        public int MinimumRestaurantCreated { get; }

        public ResourceCreateCountRequirement(int minimumRestaurantCreated)
        {
            MinimumRestaurantCreated = minimumRestaurantCreated;
        }
    }
}
