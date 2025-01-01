namespace Ganz.API.CustomMiddlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Log the incoming request path
            _logger.Log(LogLevel.Information, context.Request.Path);

            //Invoke the next middleware in the pipeline
            await _next(context);

            //Get distinct response headers
            var uniqueResponseHeaders
                = context.Response.Headers
                                  .Select(x => x.Key)
                                  .Distinct();

            //Log these headers
            _logger.Log(LogLevel.Information, string.Join("DevTube:", ", ", uniqueResponseHeaders));
            _logger.Log(LogLevel.Information, "DevTube:" + context.Request.Path);
        }
    }
}
