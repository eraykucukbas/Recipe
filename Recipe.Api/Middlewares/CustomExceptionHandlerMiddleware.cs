using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Recipe.Core.DTOs.Base;
using Recipe.Core.Exceptions;

namespace Recipe.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ClientSideException => 400,
                AuthorizationException => 401,
                ForbiddenException => 403,
                NotFoundException => 404,
                _ => 500
            };

            string message = exception.Message;

            context.Response.StatusCode = statusCode;

            _logger.LogError(exception, "An error occurred: {Message}", message);

            var response = CustomResponseDto<NoContentDto>.Fail(statusCode, message);

            var options = context.RequestServices.GetRequiredService<IOptions<JsonOptions>>().Value
                .SerializerOptions;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }

    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}