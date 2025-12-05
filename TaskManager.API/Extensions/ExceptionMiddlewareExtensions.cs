namespace TaskManager.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<TaskManager.API.Middleware.ExceptionHandlingMiddleware>();
}
