using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Validation;
using TaskManager.Communication.Responses;

namespace TaskManager.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "Unhandled exception");

            context.Response.ContentType = "application/json";
            var (statusCode, payload) = MapExceptionToResponse(ex);
            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload, options));
        }
    }

    private static (int statusCode, ErrorResponse payload) MapExceptionToResponse(Exception ex)
    {
        if (ex is ValidationException vex)
        {
            var errors = vex.Errors.Select(e => e.Message).ToList();
            return (StatusCodes.Status400BadRequest, new ErrorResponse
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Failed",
                Errors = errors
            });
        }

        if (ex is NotFoundException nfe)
        {
            return (StatusCodes.Status404NotFound, new ErrorResponse
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Message = nfe.Message
            });
        }

        if (ex is ConflictException cex)
        {
            return (StatusCodes.Status409Conflict, new ErrorResponse
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Message = cex.Message
            });
        }

        return (StatusCodes.Status500InternalServerError, new ErrorResponse
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Message = "An unexpected error occurred."
        });
    }
}