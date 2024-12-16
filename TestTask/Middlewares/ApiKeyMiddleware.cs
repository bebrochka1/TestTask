namespace TestTask.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            var requestApiKey = context.Request.Headers["X-Api-Key"];

            if (string.IsNullOrEmpty(requestApiKey)) 
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Api Key is required");
                return;
            }

            var validApiKey = _configuration.GetSection("API")["Key"];

            if (requestApiKey != validApiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid Api key");
                return;
            }

            await _next(context);
        }
    }
}
