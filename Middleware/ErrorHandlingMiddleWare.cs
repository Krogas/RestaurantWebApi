using RestaurantWebApi.Exceptions;

namespace RestaurantWebApi.Middleware
{
    public class ErrorHandlingMiddleWare : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleWare> _logger;

        public ErrorHandlingMiddleWare(ILogger<ErrorHandlingMiddleWare> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundExceptions nex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(nex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Server internal error");
            }
        }
    }
}
