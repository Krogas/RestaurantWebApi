using System.Diagnostics;

namespace RestaurantWebApi.Middleware
{
    public class WatchTimeMiddleware : IMiddleware
    {
        private readonly Stopwatch _stopWatch;
        private readonly ILogger<WatchTimeMiddleware> _logger;

        public WatchTimeMiddleware(ILogger<WatchTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            if (_stopWatch.ElapsedMilliseconds / 1000 > 4)
            {
                _logger.LogInformation(
                    $"Request {context.Request.Method} at {context.Request.Path} took {_stopWatch.ElapsedMilliseconds} ms"
                );
            }
        }
    }
}
