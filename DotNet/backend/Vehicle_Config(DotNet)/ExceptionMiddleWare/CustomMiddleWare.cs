using System.Net;
using Newtonsoft.Json;
namespace Vehicle_Config_DotNet_.ExceptionMiddleware
{
    public class CustomMiddleware : IMiddleware
    {
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(ILogger<CustomMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); // Proceed with the request pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            var result = new BaseResponseDTO<string>
            {
                ErrorCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message,
                Succeed = false
            };

            var jsonResult = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(jsonResult);
        }
    }

}
