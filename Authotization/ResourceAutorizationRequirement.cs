using Microsoft.AspNetCore.Authorization;

namespace RestaurantWebApi.Authotization
{
    public enum ResourceOperation
    {
        Create,
        Update,
        Delete,
        Read
    }

    public class ResourceAutorizationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation Operation { get; }

        public ResourceAutorizationRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }
    }
}
